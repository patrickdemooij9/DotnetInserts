using System.Globalization;
using System.Text.RegularExpressions;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Helpers;
using DotNetInserts.Core.Models;
using DotNetInserts.Core.Validation;
using DotNetInserts.Tests.Infrastructure;
using OSCADSharp.Spatial;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// Labels engraved into a part's faces.
    ///
    /// The orientation tests matter more than they look: text cut into a wall with the
    /// wrong rotation comes out mirrored, and nothing in the fit report or the bounds
    /// notices - it is only visible once the part is printed. So the mapping from side to
    /// rotation is pinned here rather than left to the snapshot, which cannot currently
    /// fail (Snapshot.UpdateMode is hardcoded true).
    /// </summary>
    public class LabelTests
    {
        private static readonly Vector3 PartSize = new(90, 70, 25);

        private static string Render(SideType? side, double depth = 0.6,
                                     double along = 0, double up = 0) =>
            LabelHelper.Render(
                new LabelDataModel
                {
                    Text = "ABC",
                    Side = side,
                    FontSize = 8,
                    Depth = depth,
                    Along = along,
                    Up = up
                },
                PartSize,
                floorZ: 4).ToString()!;

        private static readonly SideType[] Sides =
            [SideType.TOP, SideType.RIGHT, SideType.BOTTOM, SideType.LEFT];

        /// <summary>Every rotate([...]) in the emitted script, in order.</summary>
        private static List<double[]> Rotations(string scad) =>
            Regex.Matches(scad, @"rotate\(\s*v?\s*=?\s*\[([^\]]+)\]")
                 .Select(m => m.Groups[1].Value
                               .Split(',')
                               .Select(v => double.Parse(v.Trim(), CultureInfo.InvariantCulture))
                               .ToArray())
                 .ToList();

        [Fact]
        public void Each_wall_gets_its_own_orientation()
        {
            // Four walls, four different turns. If two ever collide, one of them is
            // showing mirrored or sideways text.
            var seen = Sides
                .Select(s => string.Join(",", Rotations(Render(s)).Single()))
                .ToList();

            Assert.Equal(4, seen.Distinct().Count());
        }

        [Theory]
        [InlineData(SideType.BOTTOM, 90, 0, 0)]
        [InlineData(SideType.TOP, 90, 0, 180)]
        [InlineData(SideType.RIGHT, 90, 0, 90)]
        [InlineData(SideType.LEFT, 90, 0, -90)]
        public void Wall_label_is_turned_to_face_outward(SideType side, double x, double y, double z)
        {
            // Each of these sends the extrusion out through its own wall and the glyph's
            // up to +Z. Verified visually against rendered text for all four walls; the
            // numbers are pinned so a later refactor cannot silently flip a sign.
            Assert.Equal([x, y, z], Rotations(Render(side)).Single());
        }

        [Fact]
        public void Floor_label_is_left_flat()
        {
            Assert.Empty(Rotations(Render(side: null)));
        }

        [Theory]
        [InlineData(0.6)]
        [InlineData(1.2)]
        public void Cut_breaks_the_surface_it_is_engraved_into(double depth)
        {
            // Text3D emits linear_extrude(height = 1), so the depth comes from the Z
            // scale. It has to exceed the requested depth, or the cut lands exactly
            // coplanar with the face and may leave a zero-thickness skin.
            var scale = Regex.Match(Render(SideType.BOTTOM, depth),
                                    @"scale\(\s*v?\s*=?\s*\[[^,]+,[^,]+,\s*([^\]]+)\]");

            Assert.True(scale.Success, "expected the label to be scaled to its depth");
            Assert.True(
                double.Parse(scale.Groups[1].Value, CultureInfo.InvariantCulture) > depth,
                "the cut must overshoot the face, not sit flush with it");
        }

        [Fact]
        public void Along_and_up_move_the_label_without_changing_its_orientation()
        {
            var centred = Render(SideType.BOTTOM);
            var nudged = Render(SideType.BOTTOM, along: 12, up: -5);

            Assert.NotEqual(centred, nudged);
            Assert.Equal(Rotations(centred).Single(), Rotations(nudged).Single());
        }

        /// <summary>
        /// The original cardHolder Text property engraved the floor and nothing else.
        /// Files using it must keep working now that labels are the general mechanism.
        /// </summary>
        [Fact]
        public void Legacy_cardholder_text_still_engraves_the_floor()
        {
            var holder = new CardHolder(
                new CardHolderModel(new Vector3(73, 100, 24), new Vector3(68, 94, 20))
                {
                    Text = "Events",
                    TextSize = 12
                });

            var scad = holder.ToString()!;

            Assert.Contains("\"Events\"", scad);
            // A floor label is flat, so the only turns in the script belong to the
            // holder's own cutouts - none of them is the 90 the wall labels use.
            Assert.DoesNotContain("text(\"Events\", size = 12);\n}\nrotate", scad);
        }

        [Fact]
        public void Cardholder_carries_both_a_floor_label_and_a_wall_label()
        {
            var model = new CardHolderModel(new Vector3(73, 100, 24), new Vector3(68, 94, 20))
            {
                Text = "Floor"
            };
            model.Labels.Add(new LabelDataModel { Text = "Wall", Side = SideType.RIGHT });

            var scad = new CardHolder(model).ToString()!;

            Assert.Contains("\"Floor\"", scad);
            Assert.Contains("\"Wall\"", scad);
        }

        [Fact]
        public void Labels_load_from_json_onto_both_part_types()
        {
            var insert = InsertAnalyzer.LoadFile(Fixtures.PathOf("Features/Labels"));
            var scad = InsertAnalyzer.BuildCombinedScad(insert);

            foreach (var expected in new[] { "FRONT", "BACK", "LEFT", "RIGHT", "SIDE", "Floor" })
            {
                Assert.Contains($"\"{expected}\"", scad);
            }
        }
    }
}
