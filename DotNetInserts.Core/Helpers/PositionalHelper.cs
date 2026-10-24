using DotNetInserts.Core.Enums;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Helpers
{
    public static class PositionalHelper
    {
        /// <summary>
        /// Places a cutout against one wall of a component: centred along that wall, and
        /// starting at the OUTER surface of the part, cutting inward by its own depth.
        ///
        /// So a 20 mm deep cutout removes 20 mm measured from the outside: it takes the
        /// wall and reaches the remainder into the compartment. Ask for exactly the wall
        /// thickness and you get exactly the wall.
        ///
        /// Earlier versions anchored elsewhere and were confusing for it. Straddling the
        /// wall line meant only half the depth did anything, so you had to declare double
        /// the wall thickness to break through; anchoring to the inner face and cutting
        /// outward meant the surplus was spent outside the part where it removed nothing.
        ///
        /// Works off the rendered bounds rather than the declared size, because shapes sit
        /// differently in their own coordinates - a cube starts at its origin, a scoop is
        /// centred on it.
        ///
        /// DANGER: those bounds are not always right. This fork reports a Difference's
        /// bounds as its first child's, and a Union wrapping one inherits the error -
        /// ScoopCutout is exactly that. Anything that changes a shape's REPORTED bounding
        /// box therefore moves every cutout built from it, silently and by an amount that
        /// has nothing to do with the intended change. Rotating cutouts per side to orient
        /// them once slid a 400 mm scoop 60 mm along its wall for this reason.
        ///
        /// So: reorient a shape INSIDE the shape, keeping its bounding box invariant (see
        /// ICutoutShape.Render's mirrorDepth). Never by transforming it out here.
        /// </summary>
        /// <param name="componentSize">Size of the component the cutout is made in.</param>
        /// <param name="cutout">Bounds of the cutout as rendered, after any rotation.</param>
        /// <param name="wall">Thickness of the part's wall on this side, outside the component.</param>
        public static Vector3 CalculatePositionOffset(
            Vector3 componentSize, Bounds cutout, SideType side, double wall = 0)
        {
            var offset = new Vector3();

            switch (side)
            {
                // Outer surface is at componentSize.Y + wall; the cutout's far face lands
                // there and the rest of its depth reaches back into the compartment.
                case SideType.TOP:
                    offset.X = Centre(componentSize.X, cutout.XMin, cutout.XMax);
                    offset.Y = componentSize.Y + wall - cutout.YMax;
                    break;

                // Outer surface is at -wall, and inward is +Y here.
                case SideType.BOTTOM:
                    offset.X = Centre(componentSize.X, cutout.XMin, cutout.XMax);
                    offset.Y = -wall - cutout.YMin;
                    break;

                // Left and right cutouts are rotated a quarter turn before they get here,
                // so their depth runs along X and their length along Y.
                case SideType.RIGHT:
                    offset.X = componentSize.X + wall - cutout.XMax;
                    offset.Y = Centre(componentSize.Y, cutout.YMin, cutout.YMax);
                    break;

                case SideType.LEFT:
                    offset.X = -wall - cutout.XMin;
                    offset.Y = Centre(componentSize.Y, cutout.YMin, cutout.YMax);
                    break;
            }

            return offset;
        }

        /// <summary>Shift that centres a span of [min, max] within [0, extent].</summary>
        private static double Centre(double extent, double min, double max) =>
            (extent - (min + max)) / 2;
    }
}
