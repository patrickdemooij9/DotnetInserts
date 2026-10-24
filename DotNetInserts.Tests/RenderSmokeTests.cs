using System.Diagnostics;
using System.Text;
using DotNetInserts.Core.Validation;
using DotNetInserts.Tests.Infrastructure;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// Tier 4: does OpenSCAD actually accept what we generate? Everything above works on
    /// the object graph, so it cannot catch SCAD that is syntactically fine but fails to
    /// render (a bad hull, an empty difference, a non-manifold union).
    ///
    /// Slow - tens of seconds per fixture - so it is off by default. Turn it on with
    /// RENDER_TESTS=1, and point OPENSCAD_PATH at openscad.exe if it is not in the
    /// default install location.
    /// </summary>
    public class RenderSmokeTests
    {
        public static TheoryData<string> RenderFixtures() => new()
        {
            // One simple and one geometry-heavy fixture; the point is to exercise the
            // renderer, not to re-render the whole corpus on every run.
            "Features/BoxWithWell",
            "Features/Cutouts",
            // The snap lid earns its place: its arm slots and hulled detents are the
            // fiddliest geometry the generator emits, and this is the only test that
            // would catch a hull that collapses or a slot that leaves a non-manifold edge.
            "Features/SnapCoupon",
        };

        [Theory]
        [MemberData(nameof(RenderFixtures))]
        public void Fixture_renders_to_a_solid_stl(string fixture)
        {
            if (Environment.GetEnvironmentVariable("RENDER_TESTS") is not ("1" or "true" or "TRUE"))
                return; // opt-in; see class summary

            var openscad = FindOpenScad();
            Assert.True(openscad is not null,
                "RENDER_TESTS is on but OpenSCAD was not found - set OPENSCAD_PATH to openscad.exe.");

            var workDir = Path.Combine(Path.GetTempPath(), "dotnetinserts-render-tests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(workDir);
            try
            {
                var insert = InsertAnalyzer.LoadFile(Fixtures.PathOf(fixture));

                RenderAndCheck(openscad!, workDir, fixture, "parts",
                    InsertAnalyzer.BuildCombinedScad(insert));

                // Lids are not Insert.Parts, so the combined document above does not
                // contain them. They are also the most intricate thing here, so rendering
                // only the box would miss exactly the geometry most likely to break.
                var extras = InsertAnalyzer.BuildExtraPartsScad(insert);
                if (extras.Trim() != "$fn=48;")
                {
                    RenderAndCheck(openscad!, workDir, fixture, "extras", extras);
                }
            }
            finally
            {
                try { Directory.Delete(workDir, recursive: true); } catch { /* best effort */ }
            }
        }

        private static void RenderAndCheck(
            string openscad, string workDir, string fixture, string label, string scad)
        {
            var scadPath = Path.Combine(workDir, $"{label}.scad");
            var stlPath = Path.Combine(workDir, $"{label}.stl");
            File.WriteAllText(scadPath, scad);

            var (exitCode, stderr) = Run(openscad, ["-o", stlPath, scadPath]);

            Assert.True(exitCode == 0, $"OpenSCAD exited {exitCode} rendering {fixture} ({label}):\n{stderr}");
            Assert.True(File.Exists(stlPath), $"OpenSCAD produced no STL for {fixture} ({label}).");

            Assert.True(CountTriangles(stlPath) > 0,
                $"{fixture} ({label}) rendered to an empty STL - the geometry cancelled itself out.");

            // OpenSCAD reports these on stderr but still exits 0, so check explicitly.
            Assert.DoesNotContain("WARNING: Object may not be a valid 2-manifold", stderr);
        }

        private static string? FindOpenScad()
        {
            var candidates = new[]
            {
                Environment.GetEnvironmentVariable("OPENSCAD_PATH"),
                @"C:\Program Files\OpenSCAD\openscad.exe",
                @"C:\Program Files (x86)\OpenSCAD\openscad.exe",
            };

            return candidates.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c) && File.Exists(c));
        }

        private static (int ExitCode, string StdErr) Run(string exe, string[] args)
        {
            var psi = new ProcessStartInfo(exe)
            {
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
            foreach (var a in args) psi.ArgumentList.Add(a);

            using var process = Process.Start(psi)!;
            var stderr = process.StandardError.ReadToEnd();
            process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return (process.ExitCode, stderr);
        }

        /// <summary>Triangle count from either an ASCII or a binary STL.</summary>
        private static int CountTriangles(string path)
        {
            var bytes = File.ReadAllBytes(path);
            if (bytes.Length < 84)
                return 0;

            var header = Encoding.ASCII.GetString(bytes, 0, Math.Min(80, bytes.Length));
            if (header.TrimStart().StartsWith("solid", StringComparison.Ordinal) &&
                Encoding.ASCII.GetString(bytes).Contains("facet normal", StringComparison.Ordinal))
            {
                return Encoding.ASCII.GetString(bytes).Split("facet normal").Length - 1;
            }

            return BitConverter.ToInt32(bytes, 80);
        }
    }
}
