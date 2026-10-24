using OSCADSharp.Spatial;
using System.Text.Json.Serialization;

namespace DotNetInserts.Core.JsonLoader.Models.Dimensions
{
    [JsonDerivedType(typeof(FixedDimension), typeDiscriminator: "fixed")]
    [JsonDerivedType(typeof(ConstantDimension), typeDiscriminator: "constant")]
    [JsonDerivedType(typeof(ParentAlignDimension), typeDiscriminator: "parentAlign")]
    [JsonDerivedType(typeof(PartAlignDimension), typeDiscriminator: "partAlign")]
    [JsonDerivedType(typeof(ComputedDimension), typeDiscriminator: "computed")]
    [JsonDerivedType(typeof(ComponentAlignDimension), typeDiscriminator: "componentAlign")]
    public interface IDimension
    {
        double GetSize(JsonInsertContext context);
    }
}
