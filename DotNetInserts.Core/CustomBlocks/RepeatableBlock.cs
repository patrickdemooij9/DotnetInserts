using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    public class RepeatableBlock : OSCADObject, IComponent
    {
        public RepeatableBlockModel Model { get; }

        public RepeatableBlock(RepeatableBlockModel model)
        {
            Model = model;
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), Model.Size);
        }

        public override OSCADObject Clone()
        {
            return new RepeatableBlock(Model);
        }

        public override Vector3 Position()
        {
            return new Vector3();
        }

        public override string ToString()
        {
            OSCADObject oSCADObject = null;
            List<OSCADObject> unions = new List<OSCADObject>();

            var sizeX = (Model.Size.X - (Model.WallSize * (Model.AmountX - 1))) / Model.AmountX;
            var sizeY = (Model.Size.Y - (Model.WallSize * (Model.AmountY - 1))) / Model.AmountY;
            for (var i = 0; i < Model.AmountX; i ++)
            {
                for (var j = 0; j < Model.AmountY; j++)
                {
                    var model = Model.Model;
                    model.Size = new Vector3(sizeX, sizeY, model.Size.Z);

                    if (oSCADObject is null)
                    {
                        oSCADObject = model.GetComponent() as OSCADObject;
                    }
                    else
                    {
                        unions.Add((model.GetComponent() as OSCADObject).Translate(new Vector3(model.Size.X * i + Model.WallSize * i, model.Size.Y * j + Model.WallSize * j, 0)));
                    }
                }
            }
            return oSCADObject.Union(unions.ToArray()).ToString();
        }
    }
}
