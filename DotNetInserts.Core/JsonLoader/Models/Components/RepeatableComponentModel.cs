using DotNetInserts.Core.JsonLoader.Models.Parts;

namespace DotNetInserts.Core.JsonLoader.Models.Components
{
    public class RepeatableComponentModel : IComponentModel
    {
        public const string Type = "repeatable";

        public required string Id {get; set;}
        public required string Name { get; set; }
        public required int AmountX { get; set; }
        public required int AmountY { get; set; }
        public required IComponentModel Model { get; set; }

        public required PartDimension Size { get; set; }
        public PartDimension Location { get; set; } = PartDimension.ZeroDimension();
    }
}
