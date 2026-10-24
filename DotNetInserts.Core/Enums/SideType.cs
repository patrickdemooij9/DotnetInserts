using System.Text.Json.Serialization;

namespace DotNetInserts.Core.Enums
{
    /// <summary>
    /// Written as a name in JSON ("top", "right", ...). The converter still accepts the
    /// integer form, so files predating the named values keep working.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<SideType>))]
    public enum SideType
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT
    }
}
