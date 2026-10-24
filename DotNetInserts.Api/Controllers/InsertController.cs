using DotNetInserts.Api.Models;
using DotNetInserts.Core.JsonLoader;
using DotNetInserts.Core.JsonLoader.Models.Insert;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("[controller]")]
public class InsertController : ControllerBase
{
    /// <summary>A logo is a few KB of paths; anything this size is not one.</summary>
    private const long MaxImageBytes = 2 * 1024 * 1024;

    /// <summary>
    /// Under wwwroot so an uploaded image can be served back to the builder, and beside the
    /// generated .scad files so a relative path would work too.
    /// </summary>
    private const string ImageDirectory = "wwwroot/images";

    private readonly IConfiguration _configuration;

    public InsertController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Where the browser reaches this API. Behind a reverse proxy the request's own host is
    /// the internal one, so a deployment sets PublicBaseUrl; otherwise the request is right.
    /// </summary>
    private string PublicBaseUrl =>
        (_configuration["PublicBaseUrl"] is { Length: > 0 } configured
            ? configured
            : $"{Request.Scheme}://{Request.Host}{Request.PathBase}").TrimEnd('/');

    [HttpPost]
    public async Task<IActionResult> Generate(JsonInsert insert)
    {
        // The name becomes part of every .scad and .stl path the render writes, so a name
        // like "../../x" would write outside the working directory.
        insert.Name = SafeStem(insert.Name ?? string.Empty, fallback: "insert");

        var actualInsert = new JsonInsertLoader().Load(insert);
        var output = await actualInsert.RenderStl();

        var results = output.PartStls
            .Select((stl, index) => new InsertRenderResult
            {
                InsertName = stl,
                StlFilePath = BuildStlFilePath(stl),
                IsExtra = false,
                PartIndex = index,
            })
            .Concat(output.ExtraStls.Select(extra => new InsertRenderResult
            {
                // This one is user-visible: the website renders it as the button label
                // under "Additional features", so it cannot be a wwwroot path.
                InsertName = Path.GetFileNameWithoutExtension(extra.Path),
                StlFilePath = BuildStlFilePath(extra.Path),
                IsExtra = true,
                PartIndex = extra.PartIndex,
            }));

        return Ok(results);
    }

    /// <summary>
    /// Artwork for an `svg` component. The browser cannot hand OpenSCAD a path to a file on
    /// the user's own disk, so the file is copied here once and the insert refers to the
    /// copy - which also means the saved JSON still renders after the original is moved.
    ///
    /// It lands under wwwroot so the builder can show the user which file they picked; the
    /// path handed back is absolute, because a relative one is only resolvable from
    /// whichever directory the render happens to run in.
    /// </summary>
    [HttpPost("image")]
    [RequestSizeLimit(MaxImageBytes)]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("No file was uploaded.");

        if (!string.Equals(Path.GetExtension(file.FileName), ".svg", StringComparison.OrdinalIgnoreCase))
            return BadRequest("Only .svg files can be used - OpenSCAD cannot extrude a bitmap.");

        if (file.Length > MaxImageBytes)
            return BadRequest($"That file is {file.Length / 1024} KB; the limit is {MaxImageBytes / 1024} KB.");

        using var buffer = new MemoryStream();
        await file.CopyToAsync(buffer);
        var bytes = buffer.ToArray();

        // The extension is the uploader's word for it; this is the file's own. Anything
        // without an <svg element imports as nothing, which on the finished part looks
        // exactly like an engraving the slicer swallowed.
        var head = Encoding.UTF8.GetString(bytes, 0, Math.Min(bytes.Length, 1024));
        if (!head.Contains("<svg", StringComparison.OrdinalIgnoreCase))
            return BadRequest("That file does not look like an SVG document.");

        Directory.CreateDirectory(ImageDirectory);

        // Named after the upload plus a hash of its contents, so re-uploading the same file
        // is idempotent and two different logos that happen to share a name cannot quietly
        // replace one another.
        var fileName = $"{SafeStem(Path.GetFileNameWithoutExtension(file.FileName))}-" +
                       $"{Convert.ToHexString(SHA256.HashData(bytes))[..8].ToLowerInvariant()}.svg";
        var fullPath = Path.GetFullPath(Path.Combine(ImageDirectory, fileName));

        await System.IO.File.WriteAllBytesAsync(fullPath, bytes);

        return Ok(new UploadedImageResult
        {
            ImagePath = fullPath.Replace('\\', '/'),
            PreviewUrl = $"{PublicBaseUrl}/images/{fileName}"
        });
    }

    /// <summary>
    /// Whatever is left of the uploaded name once anything that could steer the write out
    /// of the images folder is gone.
    /// </summary>
    private static string SafeStem(string name, string fallback = "image")
    {
        var stem = new string(name.Where(c => char.IsAsciiLetterOrDigit(c) || c is '-' or '_').ToArray());
        return stem.Length == 0 ? fallback : stem[..Math.Min(stem.Length, 40)];
    }

    private string BuildStlFilePath(string stl)
    {
        var publicPath = stl.Replace("wwwroot/", string.Empty);
        var hash = ComputeFileHash(stl);
        return $"{PublicBaseUrl}/{publicPath}?h={hash}";
    }

    private static string ComputeFileHash(string filePath)
    {
        if (!System.IO.File.Exists(filePath))
            return "missing";

        using var stream = System.IO.File.OpenRead(filePath);
        var bytes = SHA256.HashData(stream);
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
