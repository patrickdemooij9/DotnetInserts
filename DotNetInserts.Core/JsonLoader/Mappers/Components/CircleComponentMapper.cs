using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.Models;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Mappers.Components
{
    public class CircleComponentMapper : IComponentMapper
    {
        public Type ComponentType => typeof(CircleComponentModel);

        public IComponent Map(IComponentModel model, JsonInsertContext context)
        {
            var boxModel = (CircleComponentModel)model;

            var componentSize = new Vector3(boxModel.Size.X.GetSize(context), boxModel.Size.Y.GetSize(context), boxModel.Size.Z.GetSize(context));
            return new CircleComponent(new CircleComponentDataModel(componentSize));
        }
    }
}
