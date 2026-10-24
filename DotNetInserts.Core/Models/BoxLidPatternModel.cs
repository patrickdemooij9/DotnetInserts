using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class BoxLidPatternModel
    {
        public int Radius { get; set; }
        public int DistanceBetween { get; set; }
        public Vector3 Size { get; set; }

        public BoxLidPatternModel(Vector3 size)
        {
            Radius = 5;
            DistanceBetween = 1;
            Size = size;
        }
    }
}
