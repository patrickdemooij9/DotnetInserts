using Xunit;

namespace DotNetInserts.Tests.Infrastructure
{
    /// <summary>
    /// Minimal golden-file assertion. Set UPDATE_SNAPSHOTS=1 to rewrite the approved
    /// files, then read the git diff to decide whether the change was intended.
    /// </summary>
    public static class Snapshot
    {
        public static bool UpdateMode => Environment.GetEnvironmentVariable("UPDATE_SNAPSHOTS") is "1" or "true" or "TRUE";

        public static void Match(string relativePath, string actual)
        {
            var path = Path.Combine(TestPaths.SnapshotsDir, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            var normalised = Normalise(actual);

            if (UpdateMode || !File.Exists(path))
            {
                File.WriteAllText(path, normalised);
                if (!UpdateMode)
                    Assert.Fail($"No snapshot existed for '{relativePath}'; one was written. " +
                                "Review it, commit it, and re-run.");
                return;
            }

            var expected = Normalise(File.ReadAllText(path));
            if (expected == normalised)
                return;

            Assert.Fail($"Snapshot '{relativePath}' does not match.\n" +
                        $"{Diff(expected, normalised)}\n" +
                        "If the change is intended, re-run with UPDATE_SNAPSHOTS=1 and commit the new snapshot.");
        }

        private static string Normalise(string text) =>
            text.Replace("\r\n", "\n").TrimEnd() + "\n";

        /// <summary>First differing lines plus a little context - enough to see what moved.</summary>
        private static string Diff(string expected, string actual)
        {
            var e = expected.Split('\n');
            var a = actual.Split('\n');
            var lines = new List<string>();
            var shown = 0;

            for (var i = 0; i < Math.Max(e.Length, a.Length) && shown < 12; i++)
            {
                var el = i < e.Length ? e[i] : "<missing>";
                var al = i < a.Length ? a[i] : "<missing>";
                if (el == al)
                    continue;

                lines.Add($"  line {i + 1}:");
                lines.Add($"    expected: {el}");
                lines.Add($"    actual:   {al}");
                shown++;
            }

            if (shown == 0)
                lines.Add("  (only trailing whitespace differs)");
            else if (shown == 12)
                lines.Add("  ... more differences follow");

            return string.Join('\n', lines);
        }
    }
}
