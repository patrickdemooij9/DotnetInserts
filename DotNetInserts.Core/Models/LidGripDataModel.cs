using DotNetInserts.Core.Enums;

namespace DotNetInserts.Core.Models
{
    /// <summary>
    /// A scallop in the box's outer wall just below the lid's bottom edge, so a thumb can
    /// push the lid up.
    ///
    /// A snap lid that cannot be opened is the failure mode this exists to avoid - it is
    /// the single most common complaint about printed snap closures. It plays the same
    /// role as <see cref="InsertBoxLidDataModel.SpaceUnderLid"/> but is parametric, and
    /// does not collapse on boxes narrower than about 62 mm the way that one does.
    /// </summary>
    public class LidGripDataModel
    {
        /// <summary>
        /// Which walls get a scallop. Defaults to the pair without arms, so you push
        /// where the skirt is stiff rather than where it flexes.
        /// </summary>
        public List<SideType> Sides { get; set; } = [SideType.TOP, SideType.BOTTOM];

        /// <summary>Width of the scallop along the wall.</summary>
        public double Width { get; set; } = 20;

        /// <summary>How deep it cuts into the box's outer face.</summary>
        public double Depth { get; set; } = 1.5;
    }
}
