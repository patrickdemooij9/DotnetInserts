using System.Text.Json.Serialization;

namespace DotNetInserts.Core.JsonLoader.Models.Parts
{
    [JsonDerivedType(typeof(BoxPartModel), typeDiscriminator: BoxPartModel.Type)]
    [JsonDerivedType(typeof(CardHolderPartJsonModel), typeDiscriminator: CardHolderPartJsonModel.Type)]
    public interface IPart
    {
        string Id { get; }
        string Name { get; }
        bool Hidden { get; }
        PartDimension Size { get; }
        PartDimension Location { get; }
    }
}
