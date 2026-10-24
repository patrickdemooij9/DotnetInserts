using DotNetInserts.Core;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Spatial;
using System.Security.Cryptography;

namespace DotNetInserts.Projects.Cascadia
{
    public class CascadiaRollWriteBox
    {
        private double _wallSize = 1.2;
        private double _floorSize = 2;

        private int _boxDepth = 135;
        private int _boxLength = 185;

        //private int _cards = 34;
        private int _cards = 68;
        //private int _smallCards = 27;
        private int _smallCards = 54;

        //private double _cardSize = Constants.CardSizeTarotSleevedY;
        private double _cardSize = 0.5;
        //private double _smallCardSize = Constants.CardSizeSmallSleevedY;
        private double _smallCardSize = 0.65;

        private bool _dualSet = true;

        private int _bigDice = 21;
        private int _smallDice = 17;

        public void Render()
        {
            var insert = new Insert("CascadiaRollWrite");

            var cardHolderWithLid = GetCardsHolderWithLid();
            insert.Parts.Add(cardHolderWithLid[0], new Vector3());
            //insert.Parts.Add(cardHolderWithLid[1], new Vector3(0, 0, cardHolderWithLid[0].Bounds().Height - cardHolderWithLid[1].Bounds().Height));

            /*var diceHolderWithLid = GetDiceHoldersWithLid(_boxDepth - cardHolderWithLid[0].Bounds().YMax, cardHolderWithLid[0].Bounds().Height);
            insert.Parts.Add(diceHolderWithLid[0], new Vector3(0, cardHolderWithLid[0].Bounds().YMax, 0));
            insert.Parts.Add(diceHolderWithLid[1], new Vector3(0, cardHolderWithLid[0].Bounds().YMax, diceHolderWithLid[0].Bounds().Height - diceHolderWithLid[1].Bounds().Height));*/

            insert.Render(out _);
        }

        private OSCADObject[] GetCardsHolderWithLid()
        {
            var cardBox = new InsertBox(new Vector3(_boxLength, _boxDepth, _cardSize * _cards + _floorSize));
            /*cardBox.Lid = new InsertBoxLidDataModel(_wallSize, 1.6)
            {
                PatternData = new BoxLidPatternModel(new Vector3(cardBox.Size.X - 40, cardBox.Size.Y - 30, 4)),
                LidBoxSize = 5,
                SpaceUnderLid = 2
            };*/

            var boxDataModel = new BoxComponentDataModel(new Vector3(Constants.CardSizeTarotSleevedZ, Constants.CardSizeTarotSleevedX, cardBox.Size.Z - _floorSize));
            boxDataModel.HorizontalCutouts.Left = new CutoutCube(new Vector3(30, 15, cardBox.Size.Z + _floorSize + 1.2));
            boxDataModel.VerticalCutouts.Left = new HorizontalCutout(new Vector3(30, 25, 20), 10);
            var boxPosX = (cardBox.Size.X - boxDataModel.Size.X) / 2 - _wallSize;
            cardBox.Components.Add(new BoxComponent(boxDataModel), new ComponentLocation(new Vector3(boxPosX, -(cardBox.Size.Y - boxDataModel.Size.Y) / 2 + _wallSize)));

            var smallCardsDataModel = new BoxComponentDataModel(new Vector3(Constants.CardSizeSmallSleevedX, Constants.CardSizeSmallSleevedZ, _smallCardSize * _smallCards));
            smallCardsDataModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(30, 15, cardBox.Size.Z + _floorSize + smallCardsDataModel.Size.Z));
            cardBox.Components.Add(new BoxComponent(smallCardsDataModel), new ComponentLocation(new Vector3(-(cardBox.Size.X - smallCardsDataModel.Size.X) / 2 + _wallSize, -(cardBox.Size.Y - boxDataModel.Size.Y) / 2 + _wallSize)));

            for (var i = 0; i < (_dualSet ? 2 : 1); i++)
            {
                var bigDiceDataModel = new BoxComponentDataModel(new Vector3(_bigDice * 4, _bigDice, _bigDice));
                if (_dualSet)
                {
                    if (i == 0)
                        bigDiceDataModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(_bigDice * .9, 14, _bigDice * 2), new Vector3(0, -7));
                    else
                        bigDiceDataModel.HorizontalCutouts.Left = new CutoutCube(new Vector3(_bigDice * .9, 14, _bigDice * 2), new Vector3(0, 7));
                }
                else
                {
                    bigDiceDataModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(_bigDice * .9, 20, _bigDice * 2), new Vector3(0, -10));
                    bigDiceDataModel.HorizontalCutouts.Left = new CutoutCube(new Vector3(_bigDice * .9, 20, _bigDice * 2), new Vector3(0, 10));
                }
                var dicePosY = (cardBox.Size.Y - bigDiceDataModel.Size.Y) / 2 - _wallSize;
                var smallDiceCutoutX = _dualSet ? 15 : 20;

                var smallDiceDataModel = new BoxComponentDataModel(new Vector3(_smallDice * 4, _smallDice * 2, _smallDice));
                smallDiceDataModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(_smallDice * 1.5, smallDiceCutoutX, _smallDice * 2), new Vector3(0, -smallDiceCutoutX / 2));
                smallDiceDataModel.HorizontalCutouts.Left = new CutoutCube(new Vector3(_smallDice * 1.5, smallDiceCutoutX, _smallDice * 2), new Vector3(0, smallDiceCutoutX / 2));

                var offsetSmallDiceX = 0d;
                var offsetBigDiceX = 0d;
                if (_dualSet)
                {
                    offsetSmallDiceX = (smallDiceDataModel.Size.X / 2 + 7) * (i == 0 ? -1 : 1);
                    offsetBigDiceX = (bigDiceDataModel.Size.X / 2 + 7) * (i == 0 ? -1 : 1);
                }

                cardBox.Components.Add(new BoxComponent(bigDiceDataModel), new ComponentLocation(new Vector3(offsetBigDiceX, dicePosY)));
                cardBox.Components.Add(new BoxComponent(smallDiceDataModel), new ComponentLocation(new Vector3(offsetSmallDiceX, dicePosY - smallDiceDataModel.Size.Y + 5)));
            }

            return [cardBox];
        }

        private OSCADObject[] GetDiceHoldersWithLid(double depth, double height)
        {
            var box = new InsertBox(new Vector3(_boxLength, depth, height));
            box.Lid = new InsertBoxLidDataModel(_wallSize, 1.6)
            {
                PatternData = new BoxLidPatternModel(new Vector3(box.Size.X - 40, box.Size.Y - 30, 4)),
                LidBoxSize = 5,
                SpaceUnderLid = 2
            };

            var boxDataModel = new BoxComponentDataModel(new Vector3(box.Size.X - _wallSize * 2, box.Size.Y - _wallSize * 2, box.Size.Z - _floorSize))
            {
                RoundBottom = true
            };
            box.Components.Add(new BoxComponent(boxDataModel), new ComponentLocation(new Vector3()));

            return [box, box.GetLid().WithTolerance()];
        }
    }
}
