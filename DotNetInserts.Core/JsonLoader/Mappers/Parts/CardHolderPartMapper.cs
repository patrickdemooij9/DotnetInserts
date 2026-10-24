using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.JsonLoader.Models.Parts;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Mappers.Parts
{
    public class CardHolderPartMapper : IPartMapper
    {
        public Type PartType => typeof(CardHolderPartJsonModel);

        public OSCADObject Map(IPart part, JsonInsertContext context)
        {
            var cardHolderPart = (CardHolderPartJsonModel)part;
            
            var size = part.Size.ToVector3(context);
            var innerSize = new Vector3(cardHolderPart.InnerSize.X.GetSize(context), cardHolderPart.InnerSize.Y.GetSize(context), cardHolderPart.InnerSize.Z.GetSize(context));
            var model = new CardHolderModel(size, innerSize)
            {
                CutoutSide = cardHolderPart.CutoutSide,
                RoundedCorners = cardHolderPart.RoundedCorners,
                Text = cardHolderPart.Text,
                TextSize = cardHolderPart.TextSize,
                StackableBottom = cardHolderPart.StackableBottom,
                StackableTop = cardHolderPart.StackableTop
            };
            foreach (var label in cardHolderPart.Labels)
            {
                model.Labels.Add(label.ToDataModel());
            }
            return new CardHolder(model);
        }
    }
}
