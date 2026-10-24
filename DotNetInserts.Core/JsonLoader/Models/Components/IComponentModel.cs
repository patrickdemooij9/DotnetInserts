using DotNetInserts.Core.JsonLoader.Models.Dimensions;
using DotNetInserts.Core.JsonLoader.Models.Parts;
using OSCADSharp.Spatial;
using System.Text.Json.Serialization;

namespace DotNetInserts.Core.JsonLoader.Models.Components
{
    [JsonDerivedType(typeof(BoxComponentModel), typeDiscriminator: BoxComponentModel.Type)]
    [JsonDerivedType(typeof(RepeatableComponentModel), typeDiscriminator: RepeatableComponentModel.Type)]
    [JsonDerivedType(typeof(CircleComponentModel), typeDiscriminator: CircleComponentModel.Type)]
    [JsonDerivedType(typeof(CardTrayComponentModel), typeDiscriminator: CardTrayComponentModel.Type)]
    [JsonDerivedType(typeof(SvgComponentModel), typeDiscriminator: SvgComponentModel.Type)]
    public interface IComponentModel
    {
        string Id { get; }
        string Name { get; }
        PartDimension Size { get; }
        PartDimension Location { get; }
    }
}
