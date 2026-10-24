using System.Reflection;

namespace DotNetInserts.Tests.Infrastructure
{
    /// <summary>
    /// Resolves fixture and snapshot folders in the *source* tree rather than bin/, so
    /// that regenerating a snapshot updates the file that gets committed.
    /// </summary>
    public static class TestPaths
    {
        public static string ProjectRoot { get; } = FindProjectRoot();

        public static string FixturesDir => Path.Combine(ProjectRoot, "Fixtures");
        public static string SnapshotsDir => Path.Combine(ProjectRoot, "Snapshots");

        private static string FindProjectRoot()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            for (var current = new DirectoryInfo(dir); current != null; current = current.Parent)
            {
                if (File.Exists(Path.Combine(current.FullName, "DotNetInserts.Tests.csproj")))
                    return current.FullName;
            }

            throw new InvalidOperationException(
                $"Could not locate DotNetInserts.Tests.csproj walking up from '{dir}'.");
        }
    }
}
