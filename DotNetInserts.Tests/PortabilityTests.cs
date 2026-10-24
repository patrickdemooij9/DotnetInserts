using System.Globalization;
using DotNetInserts.Core.Validation;
using DotNetInserts.Tests.Infrastructure;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// The insert files use ',' as the decimal separator inside `expression` strings.
    /// Flee takes its separators from the ambient culture unless told otherwise, so
    /// before this was pinned the files only parsed on a comma-locale machine - they
    /// would have failed on en-US, on CI, or in a container. These tests keep that
    /// pinned, because the failure mode is silent everywhere except the one machine
    /// the files were written on.
    /// </summary>
    public class PortabilityTests
    {
        public static TheoryData<string> Cultures() => new() { "en-US", "nl-NL", "de-DE", "" };

        [Theory]
        [MemberData(nameof(Cultures))]
        public void Comma_expressions_evaluate_the_same_under_any_culture(string culture)
        {
            using var _ = new ScopedCulture(culture);

            var analysis = InsertAnalyzer.AnalyzeFile(Fixtures.PathOf("Features/Dimensions"));
            var computed = analysis.Parts.Single(p => p.Name == "Computed");

            // #Anchor.Y / 2 + 0,5  =>  100 / 2 + 0.5
            Assert.Equal(50.5, computed.Size.Y, 6);
        }

        [Theory]
        [MemberData(nameof(Cultures))]
        public void Reports_are_identical_under_any_culture(string culture)
        {
            string report;
            using (var _ = new ScopedCulture(culture))
                report = InsertAnalyzer.AnalyzeFile(Fixtures.PathOf("Features/Dimensions")).ToReport();

            using (var _ = new ScopedCulture("en-US"))
                Assert.Equal(InsertAnalyzer.AnalyzeFile(Fixtures.PathOf("Features/Dimensions")).ToReport(), report);
        }

        [Fact]
        public void Generated_scad_is_identical_across_two_loads()
        {
            // Guards against the generator picking up process-global state (OSCADSharp's
            // OutputSettings, auto-incrementing object ids) in its output, which would
            // make every snapshot flaky.
            var path = Fixtures.PathOf("Features/Cutouts");

            var first = InsertAnalyzer.BuildCombinedScad(InsertAnalyzer.LoadFile(path));
            var second = InsertAnalyzer.BuildCombinedScad(InsertAnalyzer.LoadFile(path));

            Assert.Equal(first, second);
        }

        private sealed class ScopedCulture : IDisposable
        {
            private readonly CultureInfo _previous = CultureInfo.CurrentCulture;

            public ScopedCulture(string name) =>
                CultureInfo.CurrentCulture = name.Length == 0 ? CultureInfo.InvariantCulture : new CultureInfo(name);

            public void Dispose() => CultureInfo.CurrentCulture = _previous;
        }
    }
}
