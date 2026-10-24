using DotNetInserts.Core.Enums;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    /// <summary>
    /// Vector artwork extruded into a solid, engraved into one face of a part.
    ///
    /// The same solid is also emitted as a separately printed body, so the recess and the
    /// thing that fills it can never disagree - that pair is the whole point, it is what
    /// makes a two-colour logo a slicer operation rather than a modelling one.
    /// </summary>
    public class SvgComponentDataModel
    {
        /// <summary>
        /// X/Y is the footprint the artwork is resized into, Z is how deep it cuts. The
        /// artwork is stretched to fill X/Y exactly, so give it the SVG's own aspect ratio
        /// unless distortion is what you want - nothing here can measure the file.
        /// </summary>
        public Vector3 Size { get; }

        /// <summary>
        /// Handed to OpenSCAD's import(), so it resolves relative to the directory the
        /// render runs in, not relative to the JSON file.
        /// </summary>
        public string Path { get; }

        /// <summary>Which face the artwork is cut into. Defaults to the bottom.</summary>
        public VerticalAnchor Anchor { get; }

        /// <summary>
        /// Flips the artwork in X. A logo engraved into the bottom face is read from the
        /// other side of the material, so text in it comes out backwards without this.
        /// </summary>
        public bool Mirrored { get; }

        /// <summary>
        /// Whether to also emit the solid as a separately printed body. On by default -
        /// without it the engraving is just a recess.
        /// </summary>
        public bool Export { get; }

        public SvgComponentDataModel(
            Vector3 size,
            string path,
            VerticalAnchor anchor = VerticalAnchor.BOTTOM,
            bool mirrored = false,
            bool export = true)
        {
            Size = size;
            Path = path;
            Anchor = anchor;
            Mirrored = mirrored;
            Export = export;
        }
    }
}
