using OSCADSharp;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks.CustomCutouts
{
    /// <summary>
    /// The profile of a cutout, independent of where it is placed or how deep it cuts.
    ///
    /// Render takes the height as an argument rather than reading it off the shape.
    /// The previous interface had a SetHeight that wrote back into the shape at render
    /// time, which made rendering a part mutate it - so rendering the same part twice
    /// produced different geometry. Passing the height in removes that entirely.
    /// </summary>
    public interface ICutoutShape
    {
        /// <summary>The height declared in the definition, before any mode adjustment.</summary>
        double DeclaredHeight { get; }

        /// <summary>Optional nudge applied after the shape is positioned against its wall.</summary>
        Vector3? Offset { get; }

        /// <summary>
        /// Builds the profile spanning [0, Size.X] along the wall and [0, Size.Y] into it.
        ///
        /// <paramref name="mirrorDepth"/> says which end of that depth meets the wall.
        /// The wall is at the profile's max Y for top and left cutouts, but at its min Y
        /// for bottom and right, so an asymmetric profile has to be flipped for those or
        /// the wrong face lands on the wall. Flipping here rather than rotating in
        /// BoxComponent is deliberate: the bounding box stays the same, so the placement
        /// maths - which anchors on the reported bounds - is unaffected. Shapes that are
        /// symmetric front-to-back ignore it.
        /// </summary>
        OSCADObject Render(double height, bool mirrorDepth = false);
    }
}
