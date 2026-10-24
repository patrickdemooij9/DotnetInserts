using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class ComponentLocation
    {
        public Vector3 Location { get; }
        public Vector3? Rotation { get; }
        public Vector3? Scale { get; }

        public ComponentLocation(Vector3 location, Vector3? rotation = null, Vector3? scale = null)
        {
            Location = location;
            Rotation = rotation;
            Scale = scale;
        }
    }
}
