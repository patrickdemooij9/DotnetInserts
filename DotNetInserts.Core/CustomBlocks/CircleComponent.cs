using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    public class CircleComponent : OSCADObject, IComponent
    {
        public CircleComponentDataModel Model { get; }

        public CircleComponent(CircleComponentDataModel model)
        {
            Model = model;
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), Model.Size);
        }

        public override OSCADObject Clone()
        {
            return new CircleComponent(Model);
        }

        public override Vector3 Position()
        {
            return new Vector3();
        }

        public override string ToString()
        {
            var cylinder = new Cylinder(diameter: Model.Size.X, Model.Size.Z);
            cylinder.Resolution = 100;
            return cylinder
                .Translate(Model.Size.X / 2, Model.Size.Y / 2, 0)
                .ToString()!;
        }
    }
}
