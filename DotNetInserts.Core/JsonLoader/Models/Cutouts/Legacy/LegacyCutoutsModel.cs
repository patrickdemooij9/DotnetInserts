using System.Text.Json.Serialization;
using DotNetInserts.Core.JsonLoader.Models.Parts;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Models.Cutouts.Legacy
{
    // The pre-`cutouts` schema, kept so existing insert files and anything the website
    // builder still emits keep loading unchanged. Frozen on purpose: new features go on
    // CutoutModel, not here. LegacyCutoutMapper translates these into the current model.
    //
    // The mapping is exact:
    //   verticalCutouts.<side>   -> { side, mode: notch }
    //   horizontalCutouts.<side> -> { side, mode: through }
    //   $type "horizontal"       -> $type "scoop"

    /// <summary>One cutout per wall, per collection - the limitation that motivated the rewrite.</summary>
    public class LegacyCutoutsModel
    {
        public ILegacyCutoutModel? Top { get; set; }
        public ILegacyCutoutModel? Right { get; set; }
        public ILegacyCutoutModel? Bottom { get; set; }
        public ILegacyCutoutModel? Left { get; set; }
    }

    [JsonDerivedType(typeof(LegacyCubeCutoutModel), typeDiscriminator: "cube")]
    [JsonDerivedType(typeof(LegacyScoopCutoutModel), typeDiscriminator: "horizontal")]
    public interface ILegacyCutoutModel
    {
        /// <summary>Raw millimetres - the legacy schema predates dimensions on cutouts.</summary>
        Vector3 Size { get; }

        PartDimension? Position { get; }
    }

    public class LegacyCubeCutoutModel : ILegacyCutoutModel
    {
        public required Vector3 Size { get; set; }
        public PartDimension? Position { get; set; }
        public double? RoundRadius { get; set; }
    }

    public class LegacyScoopCutoutModel : ILegacyCutoutModel
    {
        public required Vector3 Size { get; set; }
        public PartDimension? Position { get; set; }
        public double? CurveSize { get; set; }
    }
}
