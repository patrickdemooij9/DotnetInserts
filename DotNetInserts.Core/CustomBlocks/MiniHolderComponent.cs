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
    public class MiniHolderComponent : OSCADObject, IComponent
    {
        public MiniHolderComponentDataModel Model { get; }

        public MiniHolderComponent(MiniHolderComponentDataModel model)
        {
            Model = model;
        }

        public override Vector3 Position()
        {
            return new Vector3();
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), new Vector3(Model.BaseRadius, Model.Height + Model.BaseHeight, Model.BaseRadius));
        }

        public override OSCADObject Clone()
        {
            return new MiniHolderComponent(Model);
        }

        public override string ToString()
        {
            OSCADObject baseComp = new Cylinder(Model.BaseRadius, Model.BaseHeight)
                .Translate(Model.BaseRadius / 2, Model.BaseRadius / 2, 0)
                .Union(new Cube(new Vector3(Model.BaseRadius, Model.BaseRadius / 2, Model.BaseHeight + Model.HolderThickness))
                    .Translate(0, Model.BaseRadius / 2, 0))
                .Union(new Cylinder(Model.BaseRadius - 6, Model.HolderThickness)
                    .Translate((Model.BaseRadius) / 2, (Model.BaseRadius) / 2, Model.BaseHeight))
                .Union(new Cube(Model.BaseRadius, Model.BaseRadius, Model.Height)
                    .Translate(0, 0, Model.BaseHeight + Model.HolderThickness))
                .Rotate(90, 0, 180)
                .Translate(Model.BaseRadius, 0, 0);

            return baseComp.ToString();
        }
    }
}
