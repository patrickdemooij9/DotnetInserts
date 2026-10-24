using Xunit;

namespace DotNetInserts.Tests.Infrastructure
{
    /// <summary>
    /// Every .json under Fixtures/ is a test case. Dropping a new insert in that folder
    /// automatically adds it to the load, invariant and snapshot suites - no code change.
    /// </summary>
    public static class Fixtures
    {
        public static IEnumerable<string> AllPaths() =>
            Directory.EnumerateFiles(TestPaths.FixturesDir, "*.json", SearchOption.AllDirectories)
                     .OrderBy(p => p, StringComparer.Ordinal);

        /// <summary>Fixture name relative to Fixtures/, without extension, using '/'.</summary>
        public static string NameOf(string path) =>
            Path.ChangeExtension(Path.GetRelativePath(TestPaths.FixturesDir, path), null)!
                .Replace('\\', '/');

        public static string PathOf(string name) =>
            Path.Combine(TestPaths.FixturesDir, name.Replace('/', Path.DirectorySeparatorChar) + ".json");

        public static TheoryData<string> Names()
        {
            var data = new TheoryData<string>();
            foreach (var path in AllPaths())
                data.Add(NameOf(path));
            return data;
        }
    }
}
