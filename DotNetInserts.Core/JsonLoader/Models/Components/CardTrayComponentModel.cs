using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.JsonLoader.Models.Parts;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Models.Components
{
    public class CardTrayComponentModel : IComponentModel
    {
        public const string Type = "cardTray";

        public required string Id { get; set; }
        public required string Name { get; set; }

        public required PartDimension Size { get; set; }
        public PartDimension Location { get; set; } = PartDimension.ZeroDimension();

        public required Vector3 DividerSize { get; set; }
        public required double DividerWallSize { get; set; }
        public required Vector3 DividerBottomCutoutSize { get; set; }

        public required int NumberOfSlots { get; set; }
    }
}