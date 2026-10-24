using OSCADSharp;
using OSCADSharp.Spatial;
using System.Globalization;
using System.Text;

namespace DotNetInserts.Core.CustomBlocks
{
    public class ImageComponent : OSCADObject, IComponent
    {
        public string PathToImage { get; }
        public Vector3 Size { get; set; }
        public double Height { get; }

        public ImageComponent(string pathToImage, double height)
        {
            PathToImage = pathToImage;
            Height = height;
        }

        public override Vector3 Position()
        {
            throw new NotImplementedException();
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), Size);
        }

        public override OSCADObject Clone()
        {
            return new ImageComponent(PathToImage, Height);
        }

        public override string ToString()
        {
            return $"linear_extrude(height = {Height.ToString("0.00", CultureInfo.InvariantCulture)})\roffset(delta=0.001)\rimport(\"{PathToImage}\", center = true);";
        }
    }
}
