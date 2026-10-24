using System.Text.Json.Serialization;
using DotNetInserts.Core.Enums;

namespace DotNetInserts.Core.CustomBlocks.CustomCutouts
{
    /// <summary>
    /// How far down a cutout reaches. This used to be implied by which of two
    /// collections the cutout was stored in (VerticalCutouts vs HorizontalCutouts),
    /// which said nothing about what it actually did.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<CutoutMode>))]
    public enum CutoutMode
    {
        /// <summary>
        /// Cut down from the top rim of the component by the cutout's own height.
        /// The floor is left intact - this is the finger notch in a wall.
        /// </summary>
        Notch,

        /// <summary>
        /// Cut the full height of the component and break through the floor,
        /// overshooting past it by the cutout's height so the boolean leaves no skin.
        /// This is the open-bottomed channel.
        /// </summary>
        Through
    }

    /// <summary>
    /// One cutout: a shape, which wall it is cut into, and how deep it goes. Side and
    /// mode are independent of the shape, so any shape can be used in any position.
    /// </summary>
    public sealed record Cutout(SideType Side, CutoutMode Mode, ICutoutShape Shape);
}
