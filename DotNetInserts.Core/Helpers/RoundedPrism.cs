using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Helpers
{
    /// <summary>
    /// A corner-anchored box with optionally rounded vertical corners.
    ///
    /// Factored out of <see cref="CustomBlocks.InsertBox"/> so the lid can be built to
    /// the same profile as the body it sits on - a square lid on a rounded box overhangs
    /// its corners, which is what the old lid did.
    ///
    /// The construction is reproduced exactly as InsertBox had it, including the first
    /// cylinder being left at the default resolution while the other three are pinned to
    /// 50. That asymmetry is not deliberate design, but changing it would move every
    /// committed snapshot for no visual gain, so it stays until someone wants that churn.
    /// </summary>
    public static class RoundedPrism
    {
        /// <param name="diameter">
        /// Passed straight to <see cref="Cylinder"/>, whose first argument is a DIAMETER.
        /// So the corner radius is half this. Existing files rely on that reading of
        /// "roundRadius", so it is preserved rather than corrected here.
        /// </param>
        public static OSCADObject Build(Vector3 size, double? diameter)
        {
            if (diameter is null || diameter <= 0)
            {
                return new Cube(size);
            }

            var d = diameter.Value;
            return new Cylinder(d, size.Z)
                .Hull(
                    new Cylinder(d, size.Z) { Resolution = 50 }.Translate(size.X - d, 0, 0),
                    new Cylinder(d, size.Z) { Resolution = 50 }.Translate(0, size.Y - d, 0),
                    new Cylinder(d, size.Z) { Resolution = 50 }.Translate(size.X - d, size.Y - d, 0))
                .Translate(d / 2, d / 2, 0);
        }
    }
}
