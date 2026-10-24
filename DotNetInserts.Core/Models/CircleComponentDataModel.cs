using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class CircleComponentDataModel
    {
        public Vector3 Size { get; }

        public CircleComponentDataModel(Vector3 size)
        {
            Size = size;
        }
    }
}
