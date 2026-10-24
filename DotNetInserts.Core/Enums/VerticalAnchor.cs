using System.Text.Json.Serialization;

namespace DotNetInserts.Core.Enums
{
    /// <summary>
    /// Which face of the part a component's Z position is measured from.
    ///
    /// Everything else in a part hangs from the top rim, because a compartment's opening
    /// has to be flush with it. Artwork is the opposite case: it is engraved into a face,
    /// and the face that matters is usually the bottom one - the first layers of the
    /// print, which is where a filament change is free.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<VerticalAnchor>))]
    public enum VerticalAnchor
    {
        BOTTOM,
        TOP
    }
}
