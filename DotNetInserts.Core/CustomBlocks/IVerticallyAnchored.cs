using DotNetInserts.Core.Enums;

namespace DotNetInserts.Core.CustomBlocks
{
    /// <summary>
    /// A component that chooses which face of the part it is measured from. Components
    /// that do not implement this are placed flush with the top, as they always have been.
    /// </summary>
    public interface IVerticallyAnchored
    {
        VerticalAnchor Anchor { get; }
    }
}
