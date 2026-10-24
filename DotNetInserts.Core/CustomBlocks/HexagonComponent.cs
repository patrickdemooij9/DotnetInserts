using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInserts.Core.CustomBlocks
{
    public class HexagonComponent : OSCADObject, IComponent
    {
        public HexagonComponentDataModel Model { get; }

        public HexagonComponent(HexagonComponentDataModel model)
        {
            Model = model;
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), new Vector3(Model.Size, Model.Size)); //This isn't really true though
        }

        public override OSCADObject Clone()
        {
            return new HexagonComponent(Model);
        }

        public override Vector3 Position()
        {
            return new Vector3();
        }

        public override string ToString()
        {
            var cylinder = new Cylinder(Model.Size, Model.Height)
            {
                Resolution = 6
            };
            return cylinder.Translate(new Vector3(Model.Size / 2, Model.Size / 2, 0)).ToString()!;
        }
    }
}
