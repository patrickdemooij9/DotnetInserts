using DotNetInserts.Core;
using DotNetInserts.Core.JsonLoader.Models.Dimensions;

public class ConstantDimension : IDimension
{
    public required ConstantDimensionType ConstantType { get; set; }

    public double GetSize(JsonInsertContext context)
    {
        return ConstantType switch
        {
            ConstantDimensionType.CardSizeSleevedX => Constants.CardSizeSleevedX,
            ConstantDimensionType.CardSizeSleevedY => Constants.CardSizeSleevedY,
            ConstantDimensionType.CardSizeSleevedZ => Constants.CardSizeSleevedZ,
            _ => 0,
        };
    }
}