using OSCADSharp;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    /// <summary>
    /// A printed body that fills a recess in its part - the second colour of a two-colour
    /// part. Wrapping the geometry is what carries <see cref="IAlignedWithPart"/> through
    /// the transforms that place it, since a Translate returns OSCADSharp's own type and
    /// would drop any marker on the object underneath.
    /// </summary>
    public sealed class ColourInlay : OSCADObject, IAlignedWithPart
    {
        private readonly OSCADObject _geometry;

        public ColourInlay(OSCADObject geometry)
        {
            _geometry = geometry;
        }

        public override Bounds Bounds() => _geometry.Bounds();

        public override OSCADObject Clone() => new ColourInlay(_geometry.Clone());

        public override Vector3 Position() => _geometry.Position();

        public override string ToString() => _geometry.ToString()!;
    }
}
