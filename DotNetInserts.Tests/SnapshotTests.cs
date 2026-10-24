using DotNetInserts.Core.Validation;
using DotNetInserts.Tests.Infrastructure;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// Tier 2: the regression net. Nothing here asserts that the geometry is *correct* -
    /// it asserts that it has not silently changed. A refactor that shifts a wall by
    /// 0.4 mm shows up as a diff in a committed file instead of surfacing months later
    /// in a failed print.
    ///
    /// To accept a change: run with UPDATE_SNAPSHOTS=1, then read `git diff Snapshots/`.
    /// </summary>
    public class SnapshotTests
    {
        [Theory]
        [MemberData(nameof(AllFixtures))]
        public void Geometry_report_is_unchanged(string fixture)
        {
            var analysis = InsertAnalyzer.AnalyzeFile(Fixtures.PathOf(fixture));

            Snapshot.Match($"{fixture}.report.txt", analysis.ToReport());
        }

        [Theory]
        [MemberData(nameof(AllFixtures))]
        public void Generated_scad_is_unchanged(string fixture)
        {
            var insert = InsertAnalyzer.LoadFile(Fixtures.PathOf(fixture));

            Snapshot.Match($"{fixture}.scad", InsertAnalyzer.BuildCombinedScad(insert));
        }

        /// <summary>
        /// Separately-printed bodies - lids, and the dividers a `cardTray` slots in -
        /// are not Insert.Parts, so they never appear in the combined document above.
        /// Until this existed the printed lid had no snapshot at all, which is a poor
        /// place to have put a precision fit; the same argument covers a divider, whose
        /// thickness has to match the groove the tray cuts for it.
        ///
        /// Written only for fixtures that actually produce extras, so the other 20-odd
        /// fixtures do not each gain an empty file.
        /// </summary>
        [Theory]
        [MemberData(nameof(AllFixtures))]
        public void Extra_printed_parts_are_unchanged(string fixture)
        {
            var insert = InsertAnalyzer.LoadFile(Fixtures.PathOf(fixture));
            var scad = InsertAnalyzer.BuildExtraPartsScad(insert);

            if (scad.Trim() == "$fn=48;") return;

            Snapshot.Match($"{fixture}.extras.scad", scad);
        }

        public static TheoryData<string> AllFixtures() => Fixtures.Names();
    }
}
