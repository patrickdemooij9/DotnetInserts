using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Helpers;
using OSCADSharp.Spatial;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// Where a cutout lands against a wall. The rule: centred along the wall, starting at
    /// the OUTER surface of the part and cutting inward by its own depth. A 20 mm cutout
    /// removes 20 mm measured from outside - the wall, plus the remainder into the
    /// compartment. Ask for exactly the wall thickness and you get exactly the wall.
    /// </summary>
    public class PlacementTests
    {
        private static readonly Vector3 Component = new(100, 80, 30);
        private const double Wall = 4;

        /// <summary>A cube-like cutout: 20 along the wall, 10 deep, sitting at its own origin.</summary>
        private static Bounds Unrotated() => new(new Vector3(0, 0, 0), new Vector3(20, 10, 12));

        /// <summary>The same cutout after the quarter turn applied to left/right walls.</summary>
        private static Bounds Rotated() => new(new Vector3(0, 0, 0), new Vector3(-10, 20, 12));

        [Fact]
        public void Top_cutout_starts_at_the_outer_surface_and_cuts_inward()
        {
            var offset = PositionalHelper.CalculatePositionOffset(Component, Unrotated(), SideType.TOP, Wall);

            // Outer surface is at 84. The 10mm cut spans 74..84: 4 of wall, 6 into the tray.
            Assert.Equal(84, offset.Y + 10);
            Assert.Equal(74, offset.Y + 0);
            Assert.Equal(40, offset.X);   // (100 - 20) / 2, centred along the wall
        }

        [Fact]
        public void Bottom_cutout_starts_at_the_outer_surface_and_cuts_inward()
        {
            var offset = PositionalHelper.CalculatePositionOffset(Component, Unrotated(), SideType.BOTTOM, Wall);

            // Outer surface is at -4; inward is +Y. Spans -4..6.
            Assert.Equal(-4, offset.Y + 0);
            Assert.Equal(6, offset.Y + 10);
            Assert.Equal(40, offset.X);
        }

        [Fact]
        public void Right_cutout_starts_at_the_outer_surface_and_cuts_inward()
        {
            var offset = PositionalHelper.CalculatePositionOffset(Component, Rotated(), SideType.RIGHT, Wall);

            // Outer surface is at 104. Rotated bounds run x in [-10, 0], so it spans 94..104.
            Assert.Equal(104, offset.X + 0);
            Assert.Equal(94, offset.X - 10);
            Assert.Equal(30, offset.Y);   // (80 - 20) / 2
        }

        [Fact]
        public void Left_cutout_starts_at_the_outer_surface_and_cuts_inward()
        {
            var offset = PositionalHelper.CalculatePositionOffset(Component, Rotated(), SideType.LEFT, Wall);

            // Outer surface is at -4. Spans -4..6.
            Assert.Equal(-4, offset.X - 10);
            Assert.Equal(6, offset.X + 0);
            Assert.Equal(30, offset.Y);
        }

        [Fact]
        public void A_cutout_the_thickness_of_the_wall_removes_exactly_the_wall()
        {
            // The property that makes the depth number mean something: ask for the wall,
            // get the wall, with nothing spent inside the tray or outside the part.
            var exactly = new Bounds(new Vector3(0, 0, 0), new Vector3(20, Wall, 12));

            var top = PositionalHelper.CalculatePositionOffset(Component, exactly, SideType.TOP, Wall);
            var bottom = PositionalHelper.CalculatePositionOffset(Component, exactly, SideType.BOTTOM, Wall);

            Assert.Equal(Component.Y, top.Y + 0);              // inner face
            Assert.Equal(Component.Y + Wall, top.Y + Wall);    // outer face
            Assert.Equal(-Wall, bottom.Y + 0);
            Assert.Equal(0, bottom.Y + Wall);
        }

        [Fact]
        public void Every_wall_reaches_the_same_distance_into_the_compartment()
        {
            var top = PositionalHelper.CalculatePositionOffset(Component, Unrotated(), SideType.TOP, Wall);
            var bottom = PositionalHelper.CalculatePositionOffset(Component, Unrotated(), SideType.BOTTOM, Wall);
            var right = PositionalHelper.CalculatePositionOffset(Component, Rotated(), SideType.RIGHT, Wall);
            var left = PositionalHelper.CalculatePositionOffset(Component, Rotated(), SideType.LEFT, Wall);

            // 10mm deep against a 4mm wall reaches 6mm past the inner face on every side.
            Assert.Equal(6, Component.Y - (top.Y + 0));
            Assert.Equal(6, bottom.Y + 10);
            Assert.Equal(6, Component.X - (right.X - 10));
            Assert.Equal(6, left.X + 0);
        }

        [Fact]
        public void Placement_does_not_depend_on_where_the_shape_sits_in_its_own_coordinates()
        {
            // A cube starts at its origin; a scoop is centred on it. Both must reach the
            // same distance in - this is why the calculation works off rendered bounds.
            var cubeLike = new Bounds(new Vector3(0, 0, 0), new Vector3(20, 10, 12));
            var scoopLike = new Bounds(new Vector3(-10, -5, 0), new Vector3(10, 5, 12));

            var cube = PositionalHelper.CalculatePositionOffset(Component, cubeLike, SideType.TOP, Wall);
            var scoop = PositionalHelper.CalculatePositionOffset(Component, scoopLike, SideType.TOP, Wall);

            Assert.Equal(84, cube.Y + 10);     // cube's YMax is 10
            Assert.Equal(84, scoop.Y + 5);     // scoop's YMax is 5
            Assert.Equal(74, scoop.Y + -5);    // and it reaches the same 10mm inward
        }
    }
}
