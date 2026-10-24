using DotNetInserts.Core.JsonLoader.Models.Cutouts;
using DotNetInserts.Core.JsonLoader.Models.Cutouts.Legacy;
using DotNetInserts.Core.JsonLoader.Models.Parts;

namespace DotNetInserts.Core.JsonLoader.Models.Components
{
    public class BoxComponentModel : IComponentModel
    {
        public const string Type = "box";

        public required string Id { get; set; }
        public required string Name { get; set; }

        public required PartDimension Size { get; set; }
        public PartDimension Location { get; set; } = PartDimension.ZeroDimension();

        /// <summary>
        /// Cutouts, applied in order. Each one names its own wall and depth mode, so a
        /// wall can carry as many as you like.
        /// </summary>
        public CutoutModel[]? Cutouts { get; set; }

        /// <summary>Superseded by <see cref="Cutouts"/> with `mode: through`.</summary>
        public LegacyCutoutsModel? HorizontalCutouts { get; set; }

        /// <summary>Superseded by <see cref="Cutouts"/> with `mode: notch`.</summary>
        public LegacyCutoutsModel? VerticalCutouts { get; set; }

        public bool RoundBottom { get; set; }
    }
}
