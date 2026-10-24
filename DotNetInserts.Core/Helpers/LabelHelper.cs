using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Helpers
{
    /// <summary>
    /// Turns a <see cref="LabelDataModel"/> into the solid to subtract from a part.
    /// </summary>
    public static class LabelHelper
    {
        /// <summary>
        /// How far the cut reaches past the face. Without it the engraving and the face
        /// are exactly coplanar, which leaves a zero-thickness skin the slicer may or may
        /// not drop - the label then prints on some machines and not others.
        /// </summary>
        private const double Overshoot = 0.05;

        /// <param name="partSize">Outer size of the part the label is cut into.</param>
        /// <param name="floorZ">
        /// Height of the surface a floor label is engraved into. Ignored when the label
        /// names a side.
        /// </param>
        public static OSCADObject Render(LabelDataModel label, Vector3 partSize, double floorZ)
        {
            // Text3D always emits linear_extrude(height = 1), so the depth has to come
            // from a scale rather than a parameter. It also forces halign/valign to
            // centre, which is what makes anchoring to a face a matter of the part size
            // alone.
            var total = label.Depth + Overshoot;
            var text = new Text3D(label.Text, label.FontSize).Scale(1, 1, total);

            if (label.Side is null)
            {
                // Flat in the floor, read from above.
                return text.Translate(
                    partSize.X / 2 + label.Along,
                    partSize.Y / 2 + label.Up,
                    floorZ - label.Depth);
            }

            // Turned so the extrusion points OUT through the chosen face and the glyphs
            // read correctly when you look at that face from outside. The mirrored cases
            // matter: get one wrong and the text is only visibly backwards once printed.
            //
            // Each rotation sends the extrusion (+Z) through the face and the glyph's own
            // up (+Y) to world +Z; the translate then lands the slab so it spans from
            // Overshoot outside the face to Depth inside it.
            var (rotation, along, across) = label.Side switch
            {
                // front wall, -Y. Glyph +X stays world +X.
                SideType.BOTTOM => (new Vector3(90, 0, 0),
                                    partSize.X / 2 + label.Along,
                                    label.Depth),

                // back wall, +Y. Seen from outside, world -X is to the right.
                SideType.TOP => (new Vector3(90, 0, 180),
                                 partSize.X / 2 - label.Along,
                                 partSize.Y - label.Depth),

                // right wall, +X. Seen from outside, world +Y is to the right.
                SideType.RIGHT => (new Vector3(90, 0, 90),
                                   partSize.Y / 2 + label.Along,
                                   partSize.X - label.Depth),

                // left wall, -X. Seen from outside, world -Y is to the right.
                SideType.LEFT => (new Vector3(90, 0, -90),
                                  partSize.Y / 2 - label.Along,
                                  label.Depth),

                _ => throw new ArgumentOutOfRangeException(
                         nameof(label), label.Side, "Unknown label side")
            };

            var turned = text.Rotate(rotation);
            var z = partSize.Z / 2 + label.Up;

            return label.Side is SideType.LEFT or SideType.RIGHT
                ? turned.Translate(across, along, z)
                : turned.Translate(along, across, z);
        }
    }
}
