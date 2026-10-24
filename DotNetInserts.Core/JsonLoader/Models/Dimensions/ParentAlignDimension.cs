using DotNetInserts.Core.JsonLoader.Models.Dimensions;

public class ParentAlignDimension : IDimension
{
    public required AlignDimensionType AlignType { get; set; }
    public float Gap { get; set; }

    public double GetSize(JsonInsertContext context)
    {
        if (context.CurrentComponent is null)
        {
            throw new InvalidOperationException("CurrentComponent must be set in the context to get the size of an AlignDimension.");
        }
        if (context.PreventInfiniteLoop)
        {
            throw new InvalidOperationException("Prevented infinite loop to get sizes!");
        }

        context.PreventInfiniteLoop = true;
        switch (AlignType)
        {
            case AlignDimensionType.Top:
                var topValue = (context.CurrentPart.Size.Y.GetSize(context) - context.CurrentComponent.Size.Y.GetSize(context)) / 2 - Gap;
                context.PreventInfiniteLoop = false;
                return topValue;
            case AlignDimensionType.Right:
                var rightValue = (context.CurrentPart.Size.X.GetSize(context) - context.CurrentComponent.Size.X.GetSize(context)) / 2 - Gap;
                context.PreventInfiniteLoop = false;
                return rightValue;
            case AlignDimensionType.Bottom:
                var bottomValue = -(context.CurrentPart.Size.Y.GetSize(context) - context.CurrentComponent.Size.Y.GetSize(context)) / 2 + Gap;
                context.PreventInfiniteLoop = false;
                return bottomValue;
            case AlignDimensionType.Left:
                var leftValue = -(context.CurrentPart.Size.X.GetSize(context) - context.CurrentComponent.Size.X.GetSize(context)) / 2 + Gap;
                context.PreventInfiniteLoop = false;
                return leftValue;
            default:
                throw new ArgumentOutOfRangeException(nameof(AlignType), AlignType, "Invalid align dimension type.");
        }
    }
}