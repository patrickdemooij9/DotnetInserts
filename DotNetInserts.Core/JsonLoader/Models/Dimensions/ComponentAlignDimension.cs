using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.JsonLoader.Models.Dimensions;

/// <summary>
/// Places a component next to a sibling component in the same part, so a row of wells
/// can be chained without working out each one's centre by hand.
///
/// The axis this dimension sits on decides what it means. On the axis the direction
/// points along (X for Left/Right, Y for Top/Bottom) the component is butted against
/// that side of the target, <see cref="Gap"/> apart. On any other axis it takes the
/// target's own coordinate, so "right of" on both X and Y gives a component beside the
/// target and centred on it.
/// </summary>
public class ComponentAlignDimension : IDimension
{
    public required string ComponentId { get; set; }
    public required AlignDimensionType AlignType { get; set; }
    public float Gap { get; set; }

    public double GetSize(JsonInsertContext context)
    {
        var self = context.CurrentComponent
            ?? throw new InvalidOperationException("CurrentComponent must be set in the context to resolve a componentAlign dimension.");

        var axis = AxisOf(self)
            ?? throw new InvalidOperationException($"componentAlign on '{self.Name}' can only be used in a component's location.");

        var target = context.GetComponent(ComponentId);
        if (ReferenceEquals(target, self))
            throw new InvalidOperationException($"Component '{self.Name}' cannot be aligned to itself.");

        if (!context.ResolvingComponents.Add(self.Id))
            throw new InvalidOperationException($"componentAlign on '{self.Name}' refers back to itself through another component.");

        try
        {
            var targetLocation = context.WithComponent(target, () => Pick(target.Location, axis).GetSize(context));

            var pointsAlong = AlignType switch
            {
                AlignDimensionType.Left or AlignDimensionType.Right => axis == 'X',
                AlignDimensionType.Top or AlignDimensionType.Bottom => axis == 'Y',
                _ => throw new ArgumentOutOfRangeException(nameof(AlignType), AlignType, "Invalid align dimension type.")
            };
            if (!pointsAlong)
                return targetLocation;

            var targetSize = context.WithComponent(target, () => Pick(target.Size, axis).GetSize(context));
            var ownSize = Pick(self.Size, axis).GetSize(context);
            var distance = targetSize / 2 + Gap + ownSize / 2;

            return AlignType is AlignDimensionType.Right or AlignDimensionType.Top
                ? targetLocation + distance
                : targetLocation - distance;
        }
        finally
        {
            context.ResolvingComponents.Remove(self.Id);
        }
    }

    private char? AxisOf(IComponentModel component)
    {
        if (ReferenceEquals(component.Location.X, this)) return 'X';
        if (ReferenceEquals(component.Location.Y, this)) return 'Y';
        if (ReferenceEquals(component.Location.Z, this)) return 'Z';
        return null;
    }

    private static IDimension Pick(DotNetInserts.Core.JsonLoader.Models.Parts.PartDimension dimension, char axis) => axis switch
    {
        'X' => dimension.X,
        'Y' => dimension.Y,
        _ => dimension.Z
    };
}
