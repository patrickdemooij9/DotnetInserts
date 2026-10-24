namespace DotNetInserts.Core.CustomBlocks
{
    /// <summary>
    /// An extra printed body that belongs *in* its part rather than beside it - a colour
    /// inlay, not a lid.
    ///
    /// The difference is the coordinate frame. A lid is a separate object, so where it
    /// sat in the box is meaningless and it is exported at the origin. An inlay only has
    /// a use when it lines up with the part it fills, so it is exported in the same frame
    /// as the part's own STL (which carries the insert-level translation) - load both
    /// files into the slicer and they are already in register.
    /// </summary>
    public interface IAlignedWithPart
    {
    }
}
