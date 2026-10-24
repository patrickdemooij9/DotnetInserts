using DotNetInserts.Core.JsonLoader;
using DotNetInserts.Core.JsonLoader.Models.Insert;
using OSCADSharp.Spatial;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace DotNetInserts.Core.Validation
{
    public sealed class AnalyzerOptions
    {
        /// <summary>
        /// Two parts must interpenetrate by more than this on all three axes before it
        /// counts as an overlap. Parts that share a wall touch at exactly 0, and rounding
        /// in the generated geometry can push that slightly negative or positive.
        /// </summary>
        public double OverlapTolerance { get; set; } = 0.5;

        /// <summary>Slack allowed before a part counts as sticking out of the box.</summary>
        public double BoxFitTolerance { get; set; } = 0.01;

        /// <summary>
        /// Parts placed at negative coordinates are legitimate (stacked trays, extra parts
        /// laid out beside the box), so they are not reported by default.
        /// </summary>
        public bool ReportNegativeCorners { get; set; }
    }

    public static class InsertAnalyzer
    {
        /// <summary>
        /// The deserialization settings the API and website effectively use. The plain
        /// <see cref="JsonInsertLoader.Load(string)"/> overload is case-sensitive, which
        /// rejects hand-written files, so anything reading a file should go through here.
        /// </summary>
        public static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static JsonInsert ReadModel(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<JsonInsert>(json, JsonOptions)
                   ?? throw new InvalidOperationException($"'{filePath}' deserialized to null");
        }

        public static Insert LoadFile(string filePath) => new JsonInsertLoader().Load(ReadModel(filePath));

        public static InsertAnalysis AnalyzeFile(string filePath, AnalyzerOptions? options = null)
        {
            var model = ReadModel(filePath);
            return Analyze(model, new JsonInsertLoader().Load(model), options);
        }

        public static InsertAnalysis Analyze(JsonInsert model, AnalyzerOptions? options = null) =>
            Analyze(model, new JsonInsertLoader().Load(model), options);

        public static InsertAnalysis Analyze(JsonInsert model, Insert insert, AnalyzerOptions? options = null)
        {
            options ??= new AnalyzerOptions();
            var ci = CultureInfo.InvariantCulture;
            string F(double v) => Math.Round(v, 2).ToString("0.##", ci);

            // Load() skips hidden parts and preserves order, so the i-th loaded part is
            // the i-th visible part in the model.
            var visible = model.Parts.Where(p => !p.Hidden).ToList();
            var parts = new List<PartGeometry>(insert.Parts.Count);
            for (var i = 0; i < insert.Parts.Count; i++)
            {
                var loaded = insert.Parts[i];
                var b = loaded.Object.Bounds();
                var name = i < visible.Count ? visible[i].Name : $"part{i}";
                // In OSCADSharp's Bounds, Length is the X-extent and Width the Y-extent.
                parts.Add(new PartGeometry(name, loaded.Location, new Vector3(b.Length, b.Width, b.Height)));
            }

            var issues = new List<InsertIssue>();

            foreach (var p in parts)
            {
                if (p.Size.X <= 0 || p.Size.Y <= 0 || p.Size.Z <= 0)
                {
                    issues.Add(new InsertIssue(InsertIssueKind.Degenerate, p.Name,
                        $"{p.Name}: empty geometry, size is ({F(p.Size.X)}, {F(p.Size.Y)}, {F(p.Size.Z)})"));
                }

                // An axis left at 0 means the file never declared a box size on it (common
                // in the older hand-written files), so there is nothing to check against.
                var over = new StringBuilder();
                if (model.Size.X > 0 && p.MaxX > model.Size.X + options.BoxFitTolerance)
                    over.Append($" x={F(p.MaxX)}>{F(model.Size.X)}");
                if (model.Size.Y > 0 && p.MaxY > model.Size.Y + options.BoxFitTolerance)
                    over.Append($" y={F(p.MaxY)}>{F(model.Size.Y)}");
                if (model.Size.Z > 0 && p.MaxZ > model.Size.Z + options.BoxFitTolerance)
                    over.Append($" z={F(p.MaxZ)}>{F(model.Size.Z)}");

                if (options.ReportNegativeCorners)
                {
                    if (p.Location.X < -options.BoxFitTolerance) over.Append($" x={F(p.Location.X)}<0");
                    if (p.Location.Y < -options.BoxFitTolerance) over.Append($" y={F(p.Location.Y)}<0");
                    if (p.Location.Z < -options.BoxFitTolerance) over.Append($" z={F(p.Location.Z)}<0");
                }

                if (over.Length > 0)
                    issues.Add(new InsertIssue(InsertIssueKind.OutsideBox, p.Name,
                        $"{p.Name}: outside box on{over}"));
            }

            for (var i = 0; i < parts.Count; i++)
                for (var j = i + 1; j < parts.Count; j++)
                {
                    var a = parts[i];
                    var b = parts[j];
                    var ox = Math.Min(a.MaxX, b.MaxX) - Math.Max(a.Location.X, b.Location.X);
                    var oy = Math.Min(a.MaxY, b.MaxY) - Math.Max(a.Location.Y, b.Location.Y);
                    var oz = Math.Min(a.MaxZ, b.MaxZ) - Math.Max(a.Location.Z, b.Location.Z);
                    if (ox <= options.OverlapTolerance || oy <= options.OverlapTolerance || oz <= options.OverlapTolerance)
                        continue;

                    // A part fully inside another is the normal way trays get stacked into
                    // a well; only a partial intersection means two things actually clash.
                    if (Contains(a, b, options.BoxFitTolerance))
                        issues.Add(new InsertIssue(InsertIssueKind.Nested, b.Name,
                            $"{b.Name} sits inside {a.Name}"));
                    else if (Contains(b, a, options.BoxFitTolerance))
                        issues.Add(new InsertIssue(InsertIssueKind.Nested, a.Name,
                            $"{a.Name} sits inside {b.Name}"));
                    else
                        issues.Add(new InsertIssue(InsertIssueKind.Overlap, a.Name,
                            $"{a.Name} <-> {b.Name}: overlap {F(ox)} x {F(oy)} x {F(oz)} mm"));
                }

            return new InsertAnalysis
            {
                Name = insert.Name,
                BoxSize = model.Size,
                Parts = parts,
                Issues = issues,
                HiddenPartCount = model.Parts.Length - visible.Count
            };
        }

        /// <summary>True when <paramref name="inner"/>'s bounding box lies wholly within <paramref name="outer"/>'s.</summary>
        private static bool Contains(PartGeometry outer, PartGeometry inner, double tolerance) =>
            inner.Location.X >= outer.Location.X - tolerance && inner.MaxX <= outer.MaxX + tolerance &&
            inner.Location.Y >= outer.Location.Y - tolerance && inner.MaxY <= outer.MaxY + tolerance &&
            inner.Location.Z >= outer.Location.Z - tolerance && inner.MaxZ <= outer.MaxZ + tolerance;

        /// <summary>
        /// The separately-printed bodies a part produces - today that means lids.
        ///
        /// These never reach <see cref="BuildCombinedScad"/>, because they are not
        /// <see cref="Insert.Parts"/> entries; they come out of
        /// <see cref="CustomBlocks.IPartWithExtraParts.GetExtraParts"/> at render time.
        /// That left the printed lid with no snapshot and no checks at all, which is not
        /// somewhere to put a precision fit. They are laid out in a row rather than at
        /// their world positions, because they have none - an extra is a thing to print,
        /// not a thing placed in the box.
        /// </summary>
        public static string BuildExtraPartsScad(Insert insert)
        {
            var ci = CultureInfo.InvariantCulture;
            var sb = new StringBuilder();
            sb.Append("$fn=48;").Append('\n');

            double x = 0;
            foreach (var part in insert.Parts)
            {
                if (part.Object is not CustomBlocks.IPartWithExtraParts owner) continue;

                // Spaced by the owning part's own width. An extra's Bounds() cannot be
                // trusted for this - a lid is a difference of hulls, and this fork reports
                // a differenced object's bounds as its first child's - but a lid's
                // footprint is its box's footprint, which InsertBox does report honestly.
                var pitch = part.Object.Bounds().XMax + 10;

                foreach (var extra in owner.GetExtraParts())
                {
                    sb.Append($"translate([{x.ToString(ci)},0,0])").Append('\n');
                    sb.Append('{').Append('\n');
                    sb.Append(extra.ToString()).Append('\n');
                    sb.Append('}').Append('\n');
                    x += pitch;
                }
            }

            return sb.ToString().Replace("\r\n", "\n");
        }

        /// <summary>
        /// The whole insert as one OpenSCAD document, each part translated to its world
        /// position. Deterministic for a given input, which is what makes it snapshottable.
        /// </summary>
        public static string BuildCombinedScad(Insert insert)
        {
            var ci = CultureInfo.InvariantCulture;
            var sb = new StringBuilder();
            sb.Append("$fn=48;").Append('\n');
            foreach (var part in insert.Parts)
            {
                var l = part.Location;
                sb.Append($"translate([{l.X.ToString(ci)},{l.Y.ToString(ci)},{l.Z.ToString(ci)}])").Append('\n');
                sb.Append('{').Append('\n');
                sb.Append(part.Object.ToString()).Append('\n');
                sb.Append('}').Append('\n');
            }
            // Normalise line endings so snapshots do not depend on the machine.
            return sb.ToString().Replace("\r\n", "\n");
        }
    }
}
