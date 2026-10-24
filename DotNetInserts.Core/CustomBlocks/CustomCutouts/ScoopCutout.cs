using System.Diagnostics.CodeAnalysis;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks.CustomCutouts
{
    /// <summary>
    /// A finger scoop: a rounded trough with a curved lead-in on each side, so a thumb
    /// can reach under whatever is sitting in the compartment.
    ///
    /// Previously called HorizontalCutout, which collided with the HorizontalCutouts
    /// collection it could not actually be used in - it threw NotImplementedException
    /// there. Both the name and that restriction are gone: the shape works in either
    /// mode now.
    /// </summary>
    public class ScoopCutout : ICutoutShape
    {
        public required Vector3 Size { get; set; }
        public Vector3? Offset { get; set; }
        public required double CurveSize { get; set; }

        public double DeclaredHeight => Size.Z;

        public ScoopCutout()
        {
        }

        [SetsRequiredMembers]
        public ScoopCutout(Vector3 size, double curveSize, Vector3? offset = null)
        {
            Size = size;
            CurveSize = curveSize;
            Offset = offset;
        }

        public OSCADObject Render(double height, bool mirrorDepth = false)
        {
            // mirrorDepth is ignored: the trough is symmetric front-to-back, so the
            // same profile meets the wall either way round.
            var bounds = Size + new Vector3(CurveSize * 2, 0, 0);
            var sideCurve = new Cube(new Vector3(CurveSize, Size.Y, CurveSize))
                    .Difference(
                        new Cylinder(CurveSize * 2, Size.Y) { Resolution = 100 }
                        .Rotate(90, 0, 0)
                        .Translate(0, Size.Y, 0))
                    .Translate(-Size.X / 2 - CurveSize, -Size.Y / 2, height - Size.X / 2 - CurveSize);

            var value = new Cylinder(Size.X, Size.Y, center: true)
                .Difference(new Cube(Size.X * 2, Size.X, Size.Y).Translate(-Size.X, 0, -Size.Y / 2))
                .Rotate(new Vector3(x: 90))
                .Hull(new Cube(Size.X, Size.Y, height - Size.X / 2).Translate(-Size.X / 2, -Size.Y / 2, 0))
                .Union(sideCurve, sideCurve.Mirror(Size.X, 0, 0))
                .Translate(new Vector3(bounds.X / 2, 0, Size.X / 2));

            // Offset is applied by BoxComponent after placement - see CutoutCube.Render.
            return value;
        }
    }
}
