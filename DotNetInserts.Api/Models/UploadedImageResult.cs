namespace DotNetInserts.Api.Models
{
    /// <summary>
    /// Where an uploaded SVG ended up. <see cref="ImagePath"/> is what an `svg` component
    /// stores; <see cref="PreviewUrl"/> only exists so the builder can show the user which
    /// file they picked.
    /// </summary>
    public class UploadedImageResult
    {
        public required string ImagePath { get; set; }
        public required string PreviewUrl { get; set; }
    }
}
