using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;

namespace DotNetInserts.Core.JsonLoader.Models.Parts
{
    /// <summary>
    /// Text engraved into one face of a part.
    ///
    /// Everything here is a plain number rather than a dimension object. Offsets are
    /// small literals nudging a label on a face, never relational the way a part's size
    /// or location is, so the dimension machinery would only add noise - the same reason
    /// roundRadius and numberOfSlots are plain numbers.
    /// </summary>
    public class LabelModel
    {
        public required string Text { get; set; }

        /// <summary>
        /// "top" / "right" / "bottom" / "left" - which wall to cut into, where bottom is
        /// the front face. Leave it out to engrave the floor, which is what a cardHolder's
        /// text property has always done.
        /// </summary>
        public SideType? Side { get; set; }

        public int Size { get; set; } = 16;
        public double Depth { get; set; } = 0.6;
        public double Along { get; set; }
        public double Up { get; set; }

        public LabelDataModel ToDataModel() => new()
        {
            Text = Text,
            Side = Side,
            FontSize = Size,
            Depth = Depth,
            Along = Along,
            Up = Up
        };
    }
}
