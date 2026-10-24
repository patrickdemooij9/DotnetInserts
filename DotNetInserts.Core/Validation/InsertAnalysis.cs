using OSCADSharp.Spatial;
using System.Globalization;
using System.Text;

namespace DotNetInserts.Core.Validation
{
    public enum InsertIssueKind
    {
        /// <summary>Part sticks out past the declared box size on one or more axes.</summary>
        OutsideBox,
        /// <summary>
        /// Two parts partially interpenetrate. Almost always a genuine layout mistake -
        /// unlike <see cref="Nested"/>, neither part contains the other.
        /// </summary>
        Overlap,
        /// <summary>
        /// One part sits entirely inside another's bounding box. Usually deliberate (a
        /// token tray dropped into a deeper well), so this is informational.
        /// </summary>
        Nested,
        /// <summary>Part has a zero or negative extent on some axis - it renders to nothing.</summary>
        Degenerate,
    }

    public sealed record InsertIssue(InsertIssueKind Kind, string PartName, string Message);

    /// <summary>
    /// A single loaded part reduced to its world-space bounding box. Location is the
    /// part's corner; Size comes from the OSCADSharp bounds of the generated geometry.
    /// </summary>
    public sealed record PartGeometry(string Name, Vector3 Location, Vector3 Size)
    {
        public double MaxX => Location.X + Size.X;
        public double MaxY => Location.Y + Size.Y;
        public double MaxZ => Location.Z + Size.Z;
    }

    public sealed class InsertAnalysis
    {
        public required string Name { get; init; }
        public required Vector3 BoxSize { get; init; }
        public required IReadOnlyList<PartGeometry> Parts { get; init; }
        public required IReadOnlyList<InsertIssue> Issues { get; init; }
        public required int HiddenPartCount { get; init; }

        public bool IsClean => Issues.Count == 0;

        public IEnumerable<InsertIssue> IssuesOfKind(InsertIssueKind kind) =>
            Issues.Where(i => i.Kind == kind);

        /// <summary>
        /// Fixed-width text report. This is the format that gets snapshotted, so it is
        /// culture-invariant and rounded to 2 decimals - floating point noise below that
        /// would turn every unrelated change into a diff.
        /// </summary>
        public string ToReport()
        {
            var ci = CultureInfo.InvariantCulture;
            string F(double v)
            {
                var rounded = Math.Round(v, 2);
                // Avoid "-0" showing up depending on which side of zero we rounded from.
                if (rounded == 0) rounded = 0;
                return rounded.ToString("0.##", ci);
            }

            // An axis left at 0 was never declared, so say so rather than printing "0"
            // and implying every part overflows it.
            string Axis(double v) => v > 0 ? F(v) : "?";

            var sb = new StringBuilder();
            sb.Append($"insert: {Name}").Append('\n');
            sb.Append($"box:    {Axis(BoxSize.X)} x {Axis(BoxSize.Y)} x {Axis(BoxSize.Z)} mm");
            if (BoxSize.X <= 0 || BoxSize.Y <= 0 || BoxSize.Z <= 0)
                sb.Append("   (? = not declared, fit not checked on that axis)");
            sb.Append('\n');
            sb.Append($"parts:  {Parts.Count}");
            if (HiddenPartCount > 0)
                sb.Append($" (+{HiddenPartCount} hidden)");
            sb.Append('\n').Append('\n');

            sb.Append($"{"part",-24}{"corner (x,y,z)",-26}{"size (x,y,z)",-26}").Append('\n');
            sb.Append(new string('-', 76)).Append('\n');
            foreach (var p in Parts)
            {
                var corner = $"({F(p.Location.X)}, {F(p.Location.Y)}, {F(p.Location.Z)})";
                var size = $"({F(p.Size.X)}, {F(p.Size.Y)}, {F(p.Size.Z)})";
                sb.Append($"{p.Name,-24}{corner,-26}{size,-26}").Append('\n');
            }

            sb.Append('\n');
            if (Issues.Count == 0)
            {
                sb.Append("issues: none").Append('\n');
            }
            else
            {
                sb.Append($"issues: {Issues.Count}").Append('\n');
                foreach (var issue in Issues)
                    sb.Append($"  [{issue.Kind}] {issue.Message}").Append('\n');
            }

            return sb.ToString();
        }
    }
}
