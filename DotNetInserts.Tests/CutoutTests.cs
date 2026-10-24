using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;
using DotNetInserts.Core.Validation;
using DotNetInserts.Tests.Infrastructure;
using OSCADSharp.Spatial;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// The cutout model: a flat list where each entry names its own wall and depth mode.
    /// Several of these replace tests that used to pin bugs in the old two-collection
    /// design - the scoop that could not be used as a through-cut, and the render pass
    /// that mutated the model it was rendering.
    /// </summary>
    public class CutoutTests
    {
        private static BoxComponent Component(Vector3 size, params Cutout[] cutouts)
        {
            var model = new BoxComponentDataModel(size);
            model.Cutouts.AddRange(cutouts);
            return new BoxComponent(model);
        }

        /// <summary>
        /// Previously threw NotImplementedException: the scoop shape was only usable in
        /// VerticalCutouts, because BoxComponent called SetHeight on horizontal ones and
        /// the scoop did not implement it. Shape and placement are independent now.
        /// </summary>
        [Theory]
        [InlineData(CutoutMode.Notch)]
        [InlineData(CutoutMode.Through)]
        public void Scoop_works_in_both_modes(CutoutMode mode)
        {
            foreach (var side in new[] { SideType.TOP, SideType.RIGHT, SideType.BOTTOM, SideType.LEFT })
            {
                var component = Component(new Vector3(80, 60, 25),
                    new Cutout(side, mode, new ScoopCutout(new Vector3(20, 20, 10), curveSize: 4)));

                Assert.False(string.IsNullOrWhiteSpace(component.ToString()));
            }
        }

        [Theory]
        [InlineData(CutoutMode.Notch)]
        [InlineData(CutoutMode.Through)]
        public void Circle_works_in_both_modes(CutoutMode mode)
        {
            // The circle shape existed but was unreachable - no discriminator, no mapper,
            // and its height methods threw.
            var component = Component(new Vector3(80, 60, 25),
                new Cutout(SideType.TOP, mode, new CutoutCircle(new Vector3(20, 20, 10))));

            Assert.False(string.IsNullOrWhiteSpace(component.ToString()));
        }

        /// <summary>
        /// Previously each render did SetHeight(height + Model.Size.Z) straight back into
        /// the shared shape, so the second render cut deeper than the first.
        /// </summary>
        [Fact]
        public void Rendering_a_component_twice_is_idempotent()
        {
            var cutout = new CutoutCube(new Vector3(20, 20, 10));
            var component = Component(new Vector3(80, 60, 25),
                new Cutout(SideType.TOP, CutoutMode.Through, cutout));

            var first = component.ToString();
            var second = component.ToString();

            Assert.Equal(first, second);
            Assert.Equal(10, cutout.Size.Z);   // the shape was not written to
        }

        [Fact]
        public void Rendering_the_same_loaded_insert_twice_is_idempotent()
        {
            var insert = InsertAnalyzer.LoadFile(Fixtures.PathOf("Features/Cutouts"));

            Assert.Equal(
                InsertAnalyzer.BuildCombinedScad(insert),
                InsertAnalyzer.BuildCombinedScad(insert));
        }

        [Fact]
        public void Notch_cuts_only_as_deep_as_declared()
        {
            var component = Component(new Vector3(80, 60, 25),
                new Cutout(SideType.TOP, CutoutMode.Notch, new CutoutCube(new Vector3(20, 20, 10))));

            // The declared height is used as-is, so the cube appears at its declared size.
            Assert.Contains("cube(size = [20.00, 20.00, 10.00]", component.ToString());
        }

        [Fact]
        public void Through_cuts_the_full_component_height_plus_the_declared_overshoot()
        {
            var component = Component(new Vector3(80, 60, 25),
                new Cutout(SideType.TOP, CutoutMode.Through, new CutoutCube(new Vector3(20, 20, 10))));

            // 10 declared + 25 component height: clears the floor and overshoots past it.
            Assert.Contains("cube(size = [20.00, 20.00, 35.00]", component.ToString());
        }

        /// <summary>
        /// Regression: the offset used to be baked into the shape before it was
        /// positioned, so the placement anchored the already-moved shape to the wall and
        /// cancelled the offset out entirely. Offsets silently did nothing.
        /// </summary>
        [Theory]
        [InlineData(SideType.TOP)]
        [InlineData(SideType.RIGHT)]
        [InlineData(SideType.BOTTOM)]
        [InlineData(SideType.LEFT)]
        public void An_offset_actually_moves_the_cutout(SideType side)
        {
            var plain = Component(new Vector3(80, 60, 25),
                new Cutout(side, CutoutMode.Through, new CutoutCube(new Vector3(20, 10, 6))));

            var nudged = Component(new Vector3(80, 60, 25),
                new Cutout(side, CutoutMode.Through,
                    new CutoutCube(new Vector3(20, 10, 6), offset: new Vector3(7, 0, 0))));

            Assert.NotEqual(plain.ToString(), nudged.ToString());
        }

        [Fact]
        public void An_offset_does_not_change_how_deep_the_cutout_reaches()
        {
            // Nudging along the wall must not disturb the depth anchoring.
            var plain = Component(new Vector3(80, 60, 25),
                new Cutout(SideType.TOP, CutoutMode.Notch, new CutoutCube(new Vector3(20, 10, 6))));
            var nudged = Component(new Vector3(80, 60, 25),
                new Cutout(SideType.TOP, CutoutMode.Notch,
                    new CutoutCube(new Vector3(20, 10, 6), offset: new Vector3(7, 0, 0))));

            Assert.Equal(plain.Bounds().YMax, nudged.Bounds().YMax);
        }

        [Fact]
        public void A_wall_can_carry_more_than_one_cutout()
        {
            // Impossible in the old schema: the collections were keyed by side, so the
            // second cutout on a wall simply replaced the first.
            var component = Component(new Vector3(80, 60, 25),
                new Cutout(SideType.TOP, CutoutMode.Notch, new CutoutCube(new Vector3(11, 20, 10))),
                new Cutout(SideType.TOP, CutoutMode.Notch, new CutoutCube(new Vector3(13, 20, 10))));

            var scad = component.ToString();
            Assert.Contains("cube(size = [11.00, 20.00, 10.00]", scad);
            Assert.Contains("cube(size = [13.00, 20.00, 10.00]", scad);
        }

        /// <summary>
        /// The whole point of keeping the legacy shape loadable: a file written the old
        /// way and its literal translation into the new one must generate the same
        /// OpenSCAD, down to the byte. Anything less would silently reprint differently.
        /// </summary>
        [Fact]
        public void Legacy_and_modern_definitions_generate_identical_scad()
        {
            var legacy = InsertAnalyzer.BuildCombinedScad(
                InsertAnalyzer.LoadFile(Fixtures.PathOf("Features/Cutouts")));
            var modern = InsertAnalyzer.BuildCombinedScad(
                InsertAnalyzer.LoadFile(Fixtures.PathOf("Features/CutoutsModern")));

            Assert.Equal(legacy, modern);
        }

        /// <summary>
        /// AgentAvenueMigrated.json is the real AgentAvenue file put through the website
        /// builder's migrateInsert (services/CutoutMigration.ts). The two must stay
        /// interchangeable, otherwise opening an old insert in the builder and saving it
        /// would quietly reprint differently.
        /// </summary>
        [Fact]
        public void A_file_migrated_by_the_builder_generates_identical_scad()
        {
            var original = InsertAnalyzer.BuildCombinedScad(
                InsertAnalyzer.LoadFile(Fixtures.PathOf("Games/AgentAvenue")));
            var migrated = InsertAnalyzer.BuildCombinedScad(
                InsertAnalyzer.LoadFile(Fixtures.PathOf("Games/AgentAvenueMigrated")));

            Assert.Equal(original, migrated);
        }

        [Fact]
        public void Migrated_files_carry_no_legacy_cutout_properties()
        {
            var json = File.ReadAllText(Fixtures.PathOf("Games/AgentAvenueMigrated"));

            Assert.DoesNotContain("horizontalCutouts", json);
            Assert.DoesNotContain("verticalCutouts", json);
            Assert.Contains("\"cutouts\"", json);
        }

        [Fact]
        public void Side_and_mode_are_read_from_their_names()
        {
            var model = InsertAnalyzer.ReadModel(Fixtures.PathOf("Features/CutoutsModern"));
            var component = (DotNetInserts.Core.JsonLoader.Models.Components.BoxComponentModel)
                ((DotNetInserts.Core.JsonLoader.Models.Parts.BoxPartModel)model.Parts[0]).Components[0];

            Assert.NotNull(component.Cutouts);
            Assert.Contains(component.Cutouts!, c => c.Side == SideType.BOTTOM && c.Mode == CutoutMode.Through);
            Assert.Contains(component.Cutouts!, c => c.Side == SideType.TOP && c.Mode == CutoutMode.Notch);
        }
    }
}
