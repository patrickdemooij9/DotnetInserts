using DotNetInserts.Core.CustomBlocks;
using OSCADSharp;
using OSCADSharp.IO;
using OSCADSharp.Spatial;
using OSCADSharp.Utility;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace DotNetInserts.Core
{
    public record InsertPart(OSCADObject Object, Vector3 Location);

    /// <param name="PartIndex">Index into <see cref="InsertRenderOutput.PartStls"/> of the part that produced this extra.</param>
    public record ExtraStl(string Path, int PartIndex);

    public record InsertRenderOutput(List<string> PartStls, List<ExtraStl> ExtraStls);

    public class Insert
    {
        private const string CacheDir = "wwwroot/cache";

        public string Name { get; }
        public List<InsertPart> Parts { get; } = [];

        public Insert(string name)
        {
            Name = name;
        }

        public async Task<InsertRenderOutput> RenderStl()
        {
            Directory.CreateDirectory(CacheDir);

            OutputSettings.CallRenderFunction = false;

            // Sequential: generate all part .scad files (global OutputSettings state is not thread-safe)
            var partRenderInfos = new List<(string renderScad, string outputStl, string cacheStl, bool cached)>();
            for (var i = 0; i < Parts.Count; i++)
            {
                var part = Parts[i];
                var partScad = $"{Name}-{i}.scad";
                var renderScad = $"{Name}-render-{i}.scad";
                var outputStl = $"wwwroot/{Name}-{i}.stl";

                OutputSettings.RenderName = $"Render{i}";
                part.Object.ToFile(partScad);

                // Wrapper scad that applies the part's world-space translation
                File.WriteAllText(renderScad,
                    $"include <{partScad}>\n" +
                    $"translate([{part.Location.X.ToString(CultureInfo.InvariantCulture)}," +
                    $"{part.Location.Y.ToString(CultureInfo.InvariantCulture)}," +
                    $"{part.Location.Z.ToString(CultureInfo.InvariantCulture)}])\n" +
                    $"Render{i}();\n");

                var cacheHash = ComputeHashForCache(File.ReadAllText(partScad), File.ReadAllText(renderScad));
                var cacheStl = $"{CacheDir}/{cacheHash}.stl";

                var isCached = File.Exists(cacheStl);
                if (isCached)
                    File.Copy(cacheStl, outputStl, overwrite: true);

                partRenderInfos.Add((renderScad, outputStl, cacheStl, isCached));
            }

            // Sequential: generate extra part .scad files (lids, images, etc.)
            var extraRenderInfos = new List<(string renderScad, string outputStl, int partIndex)>();
            var extras = 0;
            for (var partIndex = 0; partIndex < Parts.Count; partIndex++)
            {
                var part = Parts[partIndex];
                if (part.Object is IPartWithExtraParts partWithExtraParts)
                {
                    foreach (var extraObject in partWithExtraParts.GetExtraParts())
                    {
                        OutputSettings.RenderName = $"Render{extras}";
                        var extraScad = $"{Name}-Extra-{extras}.scad";
                        // Named after the insert like the parts are. It used to be just
                        // "{n}.stl", so rendering a second insert overwrote the first
                        // one's extras and the stale URL served the wrong body.
                        var extraOutputStl = $"wwwroot/{Name}-Extra-{extras}.stl";

                        // A colour inlay is only usable in register with the part it fills,
                        // and the part's STL carries the insert-level translation - so the
                        // inlay gets the same wrapper the parts get. Everything else (lids,
                        // dividers) is a separate object and stays at the origin.
                        if (extraObject is IAlignedWithPart)
                        {
                            OutputSettings.CallRenderFunction = false;
                            extraObject.ToFile(extraScad);

                            var extraRenderScad = $"{Name}-Extra-render-{extras}.scad";
                            File.WriteAllText(extraRenderScad,
                                $"include <{extraScad}>\n" +
                                $"translate([{part.Location.X.ToString(CultureInfo.InvariantCulture)}," +
                                $"{part.Location.Y.ToString(CultureInfo.InvariantCulture)}," +
                                $"{part.Location.Z.ToString(CultureInfo.InvariantCulture)}])\n" +
                                $"Render{extras}();\n");

                            extraRenderInfos.Add((extraRenderScad, extraOutputStl, partIndex));
                        }
                        else
                        {
                            OutputSettings.CallRenderFunction = true;
                            extraObject.ToFile(extraScad);
                            extraRenderInfos.Add((extraScad, extraOutputStl, partIndex));
                        }

                        extras++;
                    }
                }
            }

            // Parallel: run all OpenSCAD processes concurrently
            var renderTasks = new List<Task>();

            for (var i = 0; i < partRenderInfos.Count; i++)
            {
                var (renderScad, outputStl, cacheStl, isCached) = partRenderInfos[i];
                if (isCached)
                    continue;

                var capturedScad = renderScad;
                var capturedStl = outputStl;
                var capturedCache = cacheStl;
                renderTasks.Add(Task.Run(async () =>
                {
                    var process = Process.Start(OutputSettings.OpenSCADPath,
                        $"-o {capturedStl} {capturedScad} --enable=fast-csg")!;
                    await process.WaitForExitAsync();
                    File.Copy(capturedStl, capturedCache, overwrite: true);
                }));
            }

            foreach (var (renderScad, outputStl, _) in extraRenderInfos)
            {
                var capturedScad = renderScad;
                var capturedStl = outputStl;
                renderTasks.Add(Task.Run(async () =>
                {
                    var process = Process.Start(OutputSettings.OpenSCADPath,
                        $"-o {capturedStl} {capturedScad} --enable=fast-csg")!;
                    await process.WaitForExitAsync();
                }));
            }

            await Task.WhenAll(renderTasks);

            return new InsertRenderOutput(
                partRenderInfos.Select(p => p.outputStl).ToList(),
                extraRenderInfos.Select(p => new ExtraStl(p.outputStl, p.partIndex)).ToList()
            );
        }

        private static string ComputeHashForCache(string partScadContent, string renderScadContent)
        {
            var input = $"{partScadContent}\n---\n{renderScadContent}";
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
