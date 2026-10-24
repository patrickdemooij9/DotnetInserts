using DotNetInserts.Core.Validation;
using DotNetInserts.Tests.Infrastructure;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// Tier 3: things that should be true of every insert regardless of what it is for.
    /// Unlike the snapshots, these fail on their own merits - you do not get to approve
    /// two trays into the same space.
    /// </summary>
    public class GeometryInvariantTests
    {
        /// <summary>
        /// Fixtures whose parts really do clash, with the reason. Anything not listed
        /// must be clean. Nested parts (one tray fully inside another) do not count -
        /// the analyzer reports those separately.
        /// </summary>
        private static readonly Dictionary<string, string> KnownOverlaps = new()
        {
            // InnerTokens sits fully inside the Tokens tray, but its twin InnerTokens2
            // runs from y=-45 to y=-4 while Tokens ends at y=-8, so it pokes 4 mm out the
            // back. Unreviewed - if that is deliberate, keep this entry; if not, fix the
            // y location in the fixture and in the source file and delete this line.
            ["Games/Deckers"] = "InnerTokens2 extends 4 mm past the back of the Tokens tray",
        };

        [Theory]
        [MemberData(nameof(AllFixtures))]
        public void No_two_parts_clash(string fixture)
        {
            var analysis = InsertAnalyzer.AnalyzeFile(Fixtures.PathOf(fixture));

            var clashes = analysis.IssuesOfKind(InsertIssueKind.Overlap).ToList();
            if (KnownOverlaps.TryGetValue(fixture, out var reason))
            {
                Assert.True(clashes.Count > 0,
                    $"{fixture} is listed in KnownOverlaps ({reason}) but no longer clashes - remove the entry.");
                return;
            }

            Assert.True(clashes.Count == 0,
                $"{fixture} has parts occupying the same space:\n" +
                string.Join('\n', clashes.Select(i => "  " + i.Message)) +
                "\nIf this is deliberate, add the fixture to KnownOverlaps with a reason.");
        }

        [Theory]
        [MemberData(nameof(AllFixtures))]
        public void Parts_fit_inside_the_declared_box(string fixture)
        {
            var analysis = InsertAnalyzer.AnalyzeFile(Fixtures.PathOf(fixture));

            var overflow = analysis.IssuesOfKind(InsertIssueKind.OutsideBox).ToList();
            Assert.True(overflow.Count == 0,
                $"{fixture} does not fit its declared box:\n" +
                string.Join('\n', overflow.Select(i => "  " + i.Message)));
        }

        [Theory]
        [MemberData(nameof(AllFixtures))]
        public void Part_names_are_unique(string fixture)
        {
            // Expressions reference parts by name (#PartName.X), so a duplicate name
            // silently resolves to whichever part comes first.
            var model = InsertAnalyzer.ReadModel(Fixtures.PathOf(fixture));

            var duplicates = model.Parts
                .GroupBy(p => p.Name, StringComparer.Ordinal)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            Assert.True(duplicates.Count == 0,
                $"{fixture} reuses part names: {string.Join(", ", duplicates)}");
        }

        public static TheoryData<string> AllFixtures() => Fixtures.Names();
    }
}
