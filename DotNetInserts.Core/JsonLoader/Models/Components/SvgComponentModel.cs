using DotNetInserts.Core.Enums;
using DotNetInserts.Core.JsonLoader.Models.Parts;

namespace DotNetInserts.Core.JsonLoader.Models.Components
{
    /// <summary>
    /// Vector artwork engraved into a face of the part, and emitted a second time as a
    /// body that fills the recess - the two-colour logo case.
    /// </summary>
    public class SvgComponentModel : IComponentModel
    {
        public const string Type = "svg";

        public required string Id { get; set; }
        public required string Name { get; set; }

        /// <summary>X/Y is the footprint the artwork is stretched into, Z is the engraving depth.</summary>
        public required PartDimension Size { get; set; }
        public PartDimension Location { get; set; } = PartDimension.ZeroDimension();

        /// <summary>
        /// Path to the .svg, passed straight to OpenSCAD's import(). It is resolved when
        /// the render runs, relative to the render's working directory - not relative to
        /// this JSON file - so an absolute path is the safe choice.
        /// </summary>
        public required string ImagePath { get; set; }

        /// <summary>Which face to cut into: the bottom of the part, or its top. Default bottom.</summary>
        public VerticalAnchor Anchor { get; set; } = VerticalAnchor.BOTTOM;

        /// <summary>Flips the artwork in X, for a face that is read from the other side.</summary>
        public bool Mirrored { get; set; }

        /// <summary>Emit the matching body alongside the lids and dividers. On by default.</summary>
        public bool Export { get; set; } = true;
    }
}
