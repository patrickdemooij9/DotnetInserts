using DotNetInserts.Core.Validation;
using DotNetInserts.Tests.Infrastructure;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// Tier 1: does every insert definition still load at all? This is the test that
    /// catches a renamed JSON property, a removed $type discriminator, or a mapper that
    /// no longer handles something the files use.
    /// </summary>
    public class LoadTests
    {
        [Theory]
        [MemberData(nameof(AllFixtures))]
        public void Fixture_loads_without_error(string fixture)
        {
            var insert = InsertAnalyzer.LoadFile(Fixtures.PathOf(fixture));

            Assert.NotEmpty(insert.Parts);
        }

        [Theory]
        [MemberData(nameof(AllFixtures))]
        public void Every_part_produces_real_geometry(string fixture)
        {
            var analysis = InsertAnalyzer.AnalyzeFile(Fixtures.PathOf(fixture));

            var empty = analysis.IssuesOfKind(InsertIssueKind.Degenerate).ToList();
            Assert.True(empty.Count == 0,
                $"{fixture} produced parts with no geometry:\n" +
                string.Join('\n', empty.Select(i => "  " + i.Message)));
        }

        [Fact]
        public void Fixture_folder_is_not_empty()
        {
            // Guards against the whole suite silently passing because the fixtures moved.
            Assert.NotEmpty(Fixtures.AllPaths());
        }

        public static TheoryData<string> AllFixtures() => Fixtures.Names();
    }
}
