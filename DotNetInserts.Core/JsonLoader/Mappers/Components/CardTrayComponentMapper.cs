using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.Models;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Mappers.Components
{
    public class CardTrayComponentMapper : IComponentMapper
    {
        public Type ComponentType => typeof(CardTrayComponentModel);

        public IComponent Map(IComponentModel model, JsonInsertContext context)
        {
            var trayModel = (CardTrayComponentModel)model;

            var cardDivider = new CardDivider(new CardDividerDataModel
            {
                Size = trayModel.DividerSize,
                WallSize = trayModel.DividerWallSize,
                BottomCutoutSize = trayModel.DividerBottomCutoutSize
            });
            return new CardTrayComponent(new CardTrayDataModel(trayModel.Size.ToVector3(context), cardDivider, trayModel.NumberOfSlots));
        }
    }
}
