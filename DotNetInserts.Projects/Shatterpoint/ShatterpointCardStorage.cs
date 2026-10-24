using DotNetInserts.Core;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Spatial;

namespace DotNetInserts.Projects.Shatterpoint
{
    public class ShatterpointCardStorage
    {
        private double _wallSize = 2;
        private double _centerWall = 5;

        private double _floorSize = 2;

        public void Render()
        {
            var insert = new Insert("ShatterpointCards");
            insert.Parts.Add(GetBox(), new Vector3());
            insert.Render(out _);
        }

        private OSCADObject GetBox()
        {
            var box = new InsertBox(new Vector3(155 + _wallSize * 2, 103 + Constants.CardSizeTarotSleevedZ + _wallSize * 2 + _centerWall, Constants.CardSizeTarotSleevedX + _floorSize), 4);

            var characterCardDataModel = new BoxComponentDataModel(new Vector3(155, 103, box.Size.Z - _floorSize));
            box.Components.Add(new BoxComponent(characterCardDataModel), new ComponentLocation(new Vector3(0, (box.Size.Y - characterCardDataModel.Size.Y) / 2 - _wallSize)));

            var dividerModel = new CardDividerDataModel()
            {
                Size = new Vector3(2, Constants.CardSizeTarotSleevedZ, Constants.CardSizeTarotSleevedX),
                WallSize = 1.5,
                BottomCutoutSize = new Vector3(2.5, 20, 2)
            };
            var tarotCardsBox = new CardTrayDataModel(new Vector3(155, Constants.CardSizeTarotSleevedZ, box.Size.Z - _floorSize), new CardDivider(dividerModel), 20);
            box.Components.Add(new CardTrayComponent(tarotCardsBox), new ComponentLocation(new Vector3(0, -(box.Size.Y - tarotCardsBox.Size.Y) / 2 + _wallSize)));

            return box;
        }
    }
}
