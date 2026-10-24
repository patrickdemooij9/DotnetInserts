using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.JsonLoader.Mappers.Cutouts;
using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.Models;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Mappers.Components
{
    public class BoxComponentMapper : IComponentMapper
    {
        public Type ComponentType => typeof(Models.Components.BoxComponentModel);

        public IComponent Map(IComponentModel model, JsonInsertContext context)
        {
            var boxModel = (Models.Components.BoxComponentModel)model;
            var cutoutMapper = new CutoutMapper();

            var componentSize = new Vector3(boxModel.Size.X.GetSize(context), boxModel.Size.Y.GetSize(context), boxModel.Size.Z.GetSize(context));
            return new BoxComponent(new BoxComponentDataModel(componentSize)
            {
                Cutouts = cutoutMapper.Map(
                    boxModel.Cutouts, boxModel.HorizontalCutouts, boxModel.VerticalCutouts, context),
                RoundBottom = boxModel.RoundBottom
            });
        }
    }
}
