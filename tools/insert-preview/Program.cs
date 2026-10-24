using System.Diagnostics;
using DotNetInserts.Core.Validation;

// insert-preview: load insert JSON through the real pipeline, print a fit/overlap
// report, and (unless --no-render) render one small iso PNG.
//
//   dotnet run --project tools/insert-preview -- <insert.json|dir> [out.png] [options]
//
// Options:
//   --no-render   skip the PNG (text report only, and much faster)
//   --strict      exit 1 if any part clashes or falls outside the declared box
//
// The analysis itself lives in DotNetInserts.Core/Validation so this tool and the
// test suite (DotNetInserts.Tests) judge geometry by exactly the same rules.
//
// Exit codes: 0 = ok, 1 = load failure (or issues found under --strict), 2 = bad usage.
// A load failure means a bad expression, an unresolved #ref, or an infinite alignment
// loop - the message says which.

var flags = args.Where(a => a.StartsWith("--")).ToHashSet();
var positional = args.Where(a => !a.StartsWith("--")).ToArray();

if (positional.Length < 1)
{
    Console.Error.WriteLine("usage: insert-preview <insert.json|dir> [out.png] [--no-render] [--strict]");
    return 2;
}

var target = positional[0];
var doRender = !flags.Contains("--no-render");
var strict = flags.Contains("--strict");

// A directory means "check all of them" - text only, since one PNG per file is rarely
// what you want when sweeping a folder.
if (Directory.Exists(target))
{
    var files = Directory.EnumerateFiles(target, "*.json", SearchOption.AllDirectories)
                         .OrderBy(f => f, StringComparer.Ordinal)
                         .ToList();
    if (files.Count == 0)
    {
        Console.Error.WriteLine($"no .json files under {target}");
        return 2;
    }

    var failed = 0;
    foreach (var file in files)
    {
        var analysis = Report(file, out var ok);
        if (!ok || (strict && analysis is { IsClean: false }))
            failed++;
        Console.WriteLine();
    }

    Console.WriteLine(strict
        ? $"{files.Count - failed}/{files.Count} loaded with no issues"
        : $"{files.Count - failed}/{files.Count} loaded (run with --strict to fail on issues)");
    return failed > 0 ? 1 : 0;
}

if (!File.Exists(target))
{
    Console.Error.WriteLine($"LOAD FAILED: file not found: {target}");
    return 1;
}

var single = Report(target, out var loaded);
if (!loaded)
    return 1;
if (strict && single is { IsClean: false })
    return 1;

if (!doRender)
    return 0;

var outPng = positional.Length >= 2 ? positional[1] : Path.ChangeExtension(target, ".preview.png");
return RenderPng(target, outPng);

// ---------------------------------------------------------------------------

InsertAnalysis? Report(string path, out bool ok)
{
    try
    {
        var analysis = InsertAnalyzer.AnalyzeFile(path);
        Console.WriteLine($"=== {path}");
        Console.WriteLine(analysis.ToReport().TrimEnd());
        ok = true;
        return analysis;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"=== {path}");
        Console.Error.WriteLine($"LOAD FAILED: {ex.GetType().Name}: {ex.Message}");
        ok = false;
        return null;
    }
}

int RenderPng(string jsonPath, string pngPath)
{
    var scadPath = Path.ChangeExtension(pngPath, ".scad");
    var loaded = InsertAnalyzer.LoadFile(jsonPath);
    File.WriteAllText(scadPath, InsertAnalyzer.BuildCombinedScad(loaded));

    // Lids and other separately-printed bodies are not Insert.Parts, so they are absent
    // from the combined document. Written alongside it, since a lid you cannot look at
    // is a lid you cannot check.
    var extras = InsertAnalyzer.BuildExtraPartsScad(loaded);
    if (extras.Trim() != "$fn=48;")
    {
        File.WriteAllText(Path.ChangeExtension(pngPath, ".extras.scad"), extras);
    }

    foreach (var candidate in new[]
             {
                 Environment.GetEnvironmentVariable("OPENSCAD_PATH"),
                 @"C:\Program Files\OpenSCAD\openscad.exe",
                 @"C:\Program Files (x86)\OpenSCAD\openscad.exe",
                 "openscad"
             })
    {
        if (string.IsNullOrWhiteSpace(candidate)) continue;
        try
        {
            var psi = new ProcessStartInfo(candidate)
            {
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
            foreach (var a in new[]
                     {
                         "-o", pngPath, "--render",
                         "--projection=ortho", "--viewall", "--autocenter",
                         "--camera=0,0,0,55,0,25,500", "--imgsize=760,540",
                         "--colorscheme=Tomorrow", scadPath
                     })
                psi.ArgumentList.Add(a);

            var proc = Process.Start(psi);
            if (proc is null) continue;
            proc.WaitForExit();
            if (proc.ExitCode == 0 && File.Exists(pngPath))
            {
                Console.WriteLine($"rendered: {pngPath}");
                return 0;
            }

            Console.Error.WriteLine($"render: OpenSCAD returned {proc.ExitCode}; {proc.StandardError.ReadToEnd()}");
            break;
        }
        catch (Exception)
        {
            // try next candidate path
        }
    }

    Console.WriteLine($"(scad written to {scadPath}; OpenSCAD not run - set OPENSCAD_PATH or use --no-render)");
    return 0;
}
