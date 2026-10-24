using DotNetInserts.Core.JsonLoader.Models.Dimensions;
using DotNetInserts.Core.JsonLoader.Models.Parts;

/// <summary>
/// Places a part next to another part of the insert - the part-level counterpart of
/// <see cref="ComponentAlignDimension"/>.
///
/// Parts are positioned by their minimum corner, not their centre. On the axis the
/// direction points along (X for Left/Right, Y for Top/Bottom) the part is butted
/// against that side of the target, <see cref="Gap"/> apart. On any other axis it takes
/// the target's coordinate, which lines up the two parts' lower edges.
/// </summary>
public class PartAlignDimension : IDimension
{
    public required string PartId { get; set; }
    public required AlignDimensionType AlignType { get; set; }
    public float Gap { get; set; }

    public double GetSize(JsonInsertContext context)
    {
        var self = context.CurrentPart;
        var axis = AxisOf(self)
            ?? throw new InvalidOperationException($"partAlign on '{self.Name}' can only be used in a part's location.");

        var target = context.Insert.Parts.FirstOrDefault(p => p.Id == PartId)
            ?? throw new ArgumentException($"Part with ID '{PartId}' not found in the insert.");
        if (ReferenceEquals(target, self))
            throw new InvalidOperationException($"Part '{self.Name}' cannot be aligned to itself.");

        if (!context.ResolvingParts.Add(self.Id))
            throw new InvalidOperationException($"partAlign on '{self.Name}' refers back to itself through another part.");

        try
        {
            var targetLocation = context.WithPart(target, () => Pick(target.Location, axis).GetSize(context));

            var pointsAlong = AlignType switch
            {
                AlignDimensionType.Left or AlignDimensionType.Right => axis == 'X',
                AlignDimensionType.Top or AlignDimensionType.Bottom => axis == 'Y',
                _ => throw new ArgumentOutOfRangeException(nameof(AlignType), AlignType, "Invalid align dimension type.")
            };
            if (!pointsAlong)
                return targetLocation;

            return AlignType is AlignDimensionType.Right or AlignDimensionType.Top
                ? targetLocation + context.WithPart(target, () => Pick(target.Size, axis).GetSize(context)) + Gap
                : targetLocation - Pick(self.Size, axis).GetSize(context) - Gap;
        }
        finally
        {
            context.ResolvingParts.Remove(self.Id);
        }
    }

    private char? AxisOf(IPart part)
    {
        if (ReferenceEquals(part.Location.X, this)) return 'X';
        if (ReferenceEquals(part.Location.Y, this)) return 'Y';
        if (ReferenceEquals(part.Location.Z, this)) return 'Z';
        return null;
    }

    private static IDimension Pick(PartDimension dimension, char axis) => axis switch
    {
        'X' => dimension.X,
        'Y' => dimension.Y,
        _ => dimension.Z
    };
}
