using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.JsonLoader.Mappers.Cutouts;
using DotNetInserts.Core.Models;

namespace DotNetInserts.Core.JsonLoader.Mappers.Components
{
    public class RepeatableComponentMapper : IComponentMapper
    {
        public Type ComponentType => typeof(RepeatableComponentModel);

        public IComponent Map(IComponentModel model, JsonInsertContext context)
        {
            var repeatableModel = (RepeatableComponentModel)model;

            var componentSize = model.Size.ToVector3(context);
            return new RepeatableBlock(new Core.Models.RepeatableBlockModel(componentSize, repeatableModel.AmountX, repeatableModel.AmountY, GetModel(repeatableModel, context)));
        }

        //TODO: This should be refactored to use the other mappers. Otherwise we have duplicate code here
        private IComponentDataModel GetModel(RepeatableComponentModel model, JsonInsertContext context)
        {
            if (model.Model is Models.Components.BoxComponentModel boxModel)
            {
                var cutoutMapper = new CutoutMapper();
                return new BoxComponentDataModel(boxModel.Size.ToVector3(context))
                {
                    Cutouts = cutoutMapper.Map(
                        boxModel.Cutouts, boxModel.HorizontalCutouts, boxModel.VerticalCutouts, context),
                    RoundBottom = boxModel.RoundBottom
                };
            }
            return null;
        }
    }
}
