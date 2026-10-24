using System.Diagnostics.CodeAnalysis;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks.CustomCutouts
{
    /// <summary>
    /// A round (or elliptical) cutout, sized by its X/Y footprint.
    ///
    /// This class existed before but was unreachable - no JSON discriminator, no mapper,
    /// and both height methods threw. It is now a first-class shape like the others.
    /// </summary>
    public class CutoutCircle : ICutoutShape
    {
        public required Vector3 Size { get; set; }
        public Vector3? Offset { get; set; }

        public double DeclaredHeight => Size.Z;

        public CutoutCircle()
        {
        }

        [SetsRequiredMembers]
        public CutoutCircle(Vector3 size, Vector3? offset = null)
        {
            Size = size;
            Offset = offset;
        }

        public OSCADObject Render(double height, bool mirrorDepth = false)
        {
            // mirrorDepth is ignored: an ellipse is symmetric front-to-back.
            // Unit cylinder scaled to the footprint, so an X != Y size gives an ellipse.
            OSCADObject shape = new Cylinder(2, height) { Resolution = 100 }
                .Scale(Size.X / 2, Size.Y / 2, 1)
                .Translate(Size.X / 2, Size.Y / 2, 0);

            // Offset is applied by BoxComponent after placement - see CutoutCube.Render.
            return shape;
        }
    }
}
