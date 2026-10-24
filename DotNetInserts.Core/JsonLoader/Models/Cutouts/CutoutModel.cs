using System.Text.Json.Serialization;
using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.JsonLoader.Models.Cutouts.Legacy;
using DotNetInserts.Core.JsonLoader.Models.Parts;

namespace DotNetInserts.Core.JsonLoader.Models.Cutouts
{
    /// <summary>
    /// A cutout in the current schema: shape (`$type`), which wall (`side`), how deep
    /// (`mode`), and a size expressed in the same dimension system as everything else.
    ///
    /// The old schema split these across two side-keyed objects, `verticalCutouts` and
    /// `horizontalCutouts`, which encoded the depth behaviour in the property name and
    /// allowed only one cutout per wall. Files in that shape still load - see
    /// <see cref="LegacyCutoutsModel"/>.
    /// </summary>
    [JsonDerivedType(typeof(CubeCutoutModel), typeDiscriminator: CubeCutoutModel.Type)]
    [JsonDerivedType(typeof(ScoopCutoutModel), typeDiscriminator: ScoopCutoutModel.Type)]
    [JsonDerivedType(typeof(CircleCutoutModel), typeDiscriminator: CircleCutoutModel.Type)]
    public abstract class CutoutModel
    {
        /// <summary>Which wall of the component the cutout is made in.</summary>
        public SideType Side { get; set; } = SideType.TOP;

        /// <summary>Notch (down from the rim) or through (full height, breaks the floor).</summary>
        public CutoutMode Mode { get; set; } = CutoutMode.Notch;

        /// <summary>
        /// X measures along the wall, Y is depth into it, Z is the cut height - which in
        /// `through` mode is how far past the floor it overshoots.
        /// </summary>
        public required PartDimension Size { get; set; }

        /// <summary>Nudge from the automatically centred position. Optional.</summary>
        public PartDimension? Offset { get; set; }

        public abstract ICutoutShape ToShape(JsonInsertContext context);
    }
}
