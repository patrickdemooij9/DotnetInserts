using System.Diagnostics.CodeAnalysis;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks.CustomCutouts
{
    /// <summary>A plain rectangular cutout, optionally with rounded vertical corners.</summary>
    public class CutoutCube : ICutoutShape
    {
        public required Vector3 Size { get; set; }
        public Vector3? Offset { get; set; }
        public double? RoundRadius { get; set; }

        public double DeclaredHeight => Size.Z;

        public CutoutCube()
        {
        }

        [SetsRequiredMembers]
        public CutoutCube(Vector3 size, Vector3? offset = null, double? roundRadius = null)
        {
            Size = size;
            Offset = offset;
            RoundRadius = roundRadius;
        }

        public OSCADObject Render(double height, bool mirrorDepth = false)
        {
            OSCADObject cube;
            if (!RoundRadius.HasValue)
            {
                cube = new Cube(new Vector3(Size.X, Size.Y, height));
            }
            else
            {
                // Rounded at one end of the depth, square across the other. Four rounded
                // corners used to leave slivers of wall standing, because the corner radii
                // are tangent to the wall plane and pinch the opening in exactly where it
                // needs to be widest.
                //
                // The square face is the one that meets the wall, so which end it sits at
                // depends on the side - see ICutoutShape.Render. Both orientations occupy
                // [0, Size.X] x [0, Size.Y] exactly, so flipping cannot move the cutout.
                var radius = RoundRadius.Value / 2;
                var roundedCentreY = mirrorDepth ? Size.Y - radius : radius;
                var squareFaceY = mirrorDepth ? 0 : Size.Y - 1;

                cube = new Cylinder(RoundRadius.Value, height) { Resolution = 50 }
                    .Translate(radius, roundedCentreY, 0)
                    .Hull(
                        new Cylinder(RoundRadius.Value, height) { Resolution = 50 }
                            .Translate(Size.X - radius, roundedCentreY, 0),
                        new Cube(new Vector3(Size.X, 1, height)).Translate(0, squareFaceY, 0));
            }

            // Offset is deliberately NOT applied here - BoxComponent adds it after
            // positioning the shape against its wall. Baking it in would make it part of
            // the bounds the placement anchors to, which cancels it out exactly.
            return cube;
        }
    }
}
