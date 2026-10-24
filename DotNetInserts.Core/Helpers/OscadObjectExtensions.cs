using OSCADSharp;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Helpers
{
    public static class OscadObjectExtensions
    {
        public static OSCADObject RotateAroundItself(this OSCADObject oscadObject, Vector3 rotation)
        {
            return oscadObject
                .Translate(-oscadObject.Bounds().XMax / 2, -oscadObject.Bounds().YMax / 2, 0)
                .Rotate(rotation)
                .Translate(oscadObject.Bounds().XMax / 2, oscadObject.Bounds().YMax / 2, 0);
        }
    }
}
