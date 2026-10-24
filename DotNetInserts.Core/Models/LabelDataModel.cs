using DotNetInserts.Core.Enums;

namespace DotNetInserts.Core.Models
{
    /// <summary>
    /// Text engraved into one face of a part.
    ///
    /// A label is a property of the part rather than a component, because components are
    /// positioned by <see cref="CustomBlocks.InsertBox"/> from their Bounds() and
    /// Text3D.Bounds() throws - a text component would have to declare an extent nobody
    /// can measure. Anchoring to a named face needs only the part size, and Text3D
    /// already centres itself on its origin.
    /// </summary>
    public class LabelDataModel
    {
        public required string Text { get; set; }

        /// <summary>
        /// Which outer face the text is cut into. Null engraves the floor instead, which
        /// is what cardHolder's Text property has always done.
        /// </summary>
        public SideType? Side { get; set; }

        /// <summary>Text3D's size: the ascent above the baseline, in mm.</summary>
        public int FontSize { get; set; } = 16;

        /// <summary>How far the engraving cuts into the face.</summary>
        public double Depth { get; set; } = 0.6;

        /// <summary>
        /// Nudge across the face, following the same convention as cutouts: it runs along
        /// the wall the label sits on, so X on the front and back walls and Y on the left
        /// and right ones. Positive is rightwards as you look at that face. Zero centres.
        /// </summary>
        public double Along { get; set; }

        /// <summary>
        /// Nudge up the face. Zero puts the label halfway up the part, or - for a floor
        /// label, where there is no "up" - leaves it centred front to back.
        /// </summary>
        public double Up { get; set; }
    }
}
