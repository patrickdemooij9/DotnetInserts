using System.Text.RegularExpressions;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;
using DotNetInserts.Core.Validation;
using DotNetInserts.Tests.Infrastructure;
using OSCADSharp.Spatial;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// Artwork engraved into a face, and the body printed to fill the recess.
    ///
    /// The pair is the whole feature, so the test that earns its place is the one that
    /// says the two cannot drift: same solid, same position, no clearance. A recess and
    /// an inlay that disagree by half a millimetre look fine in every report here and
    /// only show up once both are on the plate.
    /// </summary>
    public class SvgImageTests
    {
        private static readonly Vector3 PartSize = new(100, 80, 30);

        private static InsertBox BoxWith(SvgComponentDataModel model)
        {
            var box = new InsertBox(PartSize);
            box.Components.Add(new SvgComponent(model), new ComponentLocation(new Vector3()));
            return box;
        }

        /// <summary>Whitespace-insensitive, because the part indents its copy inside difference().</summary>
        private static string Squash(string scad) => Regex.Replace(scad, @"\s+", "");

        /// <summary>Every translate([...]) in the emitted script, in order.</summary>
        private static List<string> Translations(string scad) =>
            Regex.Matches(scad, @"translate\(v = \[([^\]]+)\]\)")
                 .Select(m => m.Groups[1].Value)
                 .ToList();

        [Fact]
        public void The_inlay_is_the_same_solid_in_the_same_place_as_the_recess()
        {
            var insert = InsertAnalyzer.LoadFile(Fixtures.PathOf("Features/SvgImage"));
            var box = (InsertBox)insert.Parts.Single().Object;

            var inlays = box.GetExtraParts().OfType<ColourInlay>().ToList();
            var part = Squash(box.ToString());

            // Two of the fixture's three images are exported; the third sets export: false.
            Assert.Equal(2, inlays.Count);
            foreach (var inlay in inlays)
                Assert.Contains(Squash(inlay.ToString()!), part);
        }

        [Fact]
        public void Export_false_engraves_without_printing_anything()
        {
            var box = BoxWith(new SvgComponentDataModel(new Vector3(40, 20, 0.6), "Logo.svg", export: false));

            Assert.Empty(box.GetExtraParts().OfType<ColourInlay>());
            Assert.Contains("Logo.svg", box.ToString());
        }

        [Theory]
        [InlineData(VerticalAnchor.BOTTOM, 0)]
        [InlineData(VerticalAnchor.TOP, 29.4)]
        public void Anchor_chooses_which_face_is_cut(VerticalAnchor anchor, double expectedZ)
        {
            // Everything else in a part hangs from the top rim. Artwork defaults to the
            // bottom face instead, which is the one a filament change is free on.
            var box = BoxWith(new SvgComponentDataModel(new Vector3(40, 20, 0.6), "Logo.svg", anchor));

            var placement = Regex.Match(box.ToString(), @"translate\(v = \[30\.00, 30\.00, ([\-0-9.]+)\]\)");

            Assert.True(placement.Success, "the image was not centred in X/Y as components are");
            Assert.Equal(expectedZ, double.Parse(placement.Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture), 2);
        }

        [Fact]
        public void Mirroring_is_applied_around_the_centre_so_it_cannot_move_the_engraving()
        {
            var size = new Vector3(40, 20, 0.6);
            var plain = BoxWith(new SvgComponentDataModel(size, "Logo.svg")).ToString();
            var mirrored = BoxWith(new SvgComponentDataModel(size, "Logo.svg", mirrored: true)).ToString();

            Assert.DoesNotContain("mirror", plain);
            Assert.Contains("mirror([1.00, 0.00, 0.00])", mirrored);

            // The import is centred on the origin, so the flip happens before the shape is
            // moved into place: every translate stays where it was. If mirroring ever moved
            // the artwork, the engraving and its inlay would still agree - they share this
            // code - but both would have slid off the face by half their width.
            Assert.Equal(Translations(plain), Translations(mirrored));
        }
    }
}
