using DotNetInserts.Core.JsonLoader.Models.Parts;

namespace DotNetInserts.Core.JsonLoader.Models.Components
{
    public class CircleComponentModel : IComponentModel
    {
        public const string Type = "circle";

        public required string Id { get; set; }
        public required string Name { get; set; }
        public required PartDimension Size { get; set; }
        public required PartDimension Location { get; set; }
    }
}
