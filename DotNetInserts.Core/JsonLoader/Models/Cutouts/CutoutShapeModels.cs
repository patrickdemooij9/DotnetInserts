using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Models.Cutouts
{
    /// <summary>A rectangular cutout, optionally with rounded vertical corners.</summary>
    public class CubeCutoutModel : CutoutModel
    {
        public const string Type = "cube";

        public double? RoundRadius { get; set; }

        public override ICutoutShape ToShape(JsonInsertContext context) => new CutoutCube
        {
            Size = Size.ToVector3(context),
            Offset = Offset?.ToVector3(context),
            RoundRadius = RoundRadius
        };
    }

    /// <summary>A finger scoop with a curved lead-in. Was `$type: "horizontal"`.</summary>
    public class ScoopCutoutModel : CutoutModel
    {
        public const string Type = "scoop";

        /// <summary>Radius of the lead-in curve either side of the trough.</summary>
        public double? CurveSize { get; set; }

        public override ICutoutShape ToShape(JsonInsertContext context) => new ScoopCutout
        {
            Size = Size.ToVector3(context),
            Offset = Offset?.ToVector3(context),
            CurveSize = CurveSize ?? 10
        };
    }

    /// <summary>A round or elliptical cutout, sized by its X/Y footprint.</summary>
    public class CircleCutoutModel : CutoutModel
    {
        public const string Type = "circle";

        public override ICutoutShape ToShape(JsonInsertContext context) => new CutoutCircle
        {
            Size = Size.ToVector3(context),
            Offset = Offset?.ToVector3(context)
        };
    }
}
