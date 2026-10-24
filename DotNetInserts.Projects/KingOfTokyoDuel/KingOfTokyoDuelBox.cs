using DotNetInserts.Core;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInserts.Projects.KingOfTokyoDuel
{
    public class KingOfTokyoDuelBox
    {
        private double _wallSize = 1.2;
        private double _floorSize = 2;

        private int _boxDepth = 151;
        private int _boxLength = 197;
        private int _boxHeight = 43;

        private int _cards = 53; // Includes promos

        private double _playerCardX = 95;
        private double _playerCardY = 121;
        private double _playerCardZ = 2;
        private int _playerCards = 6;

        private int _diceSize = 20;
        private int _dices = 8;

        private int _diceTokenSize = 19;
        private int _diceTokenHeight = 11;

        private double _lifeCounterSize = 71;

        public void Render()
        {
            var insert = new Insert("KingOfTokyoDuel");

            var diceAndTokens = RenderDiceAndTokens();
            insert.Parts.Add(diceAndTokens, new Vector3());

            var cardTray = RenderCardTray(_boxLength - diceAndTokens.Bounds().XMax);
            insert.Parts.Add(cardTray, new Vector3(diceAndTokens.Bounds().XMax, _boxDepth - cardTray.Bounds().YMax));
            //insert.Parts.Add(RenderPlayerBoard(_boxHeight - diceAndTokens.Bounds().Height), new Vector3(z: diceAndTokens.Bounds().Height));

            var tokenBoxX = _boxLength - diceAndTokens.Bounds().XMax;
            var tokenBoxY = _boxDepth - cardTray.Bounds().YMax;
            insert.Parts.Add(RenderTokenBox1(tokenBoxX, tokenBoxY, (double)_boxHeight / 2), new Vector3(diceAndTokens.Bounds().XMax));
            insert.Parts.Add(RenderTokenBox2(tokenBoxX, tokenBoxY, (double)_boxHeight / 2), new Vector3(diceAndTokens.Bounds().XMax, 0, (double)_boxHeight / 2));
            //insert.Parts.Add(TestMarkers(), new Vector3());

            insert.Render(out _);
        }

        private OSCADObject RenderDiceAndTokens()
        {
            //var box = new InsertBox(new Vector3(_playerCardX + _wallSize * 2, _boxDepth, _diceSize + _floorSize));
            var box = new InsertBox(new Vector3(_playerCardX + _wallSize * 2, _boxDepth, _boxHeight));

            var playerBoardHeight = _boxHeight - _diceSize - _floorSize * 2;
            var playerBoardModel = new BoxComponentDataModel(new Vector3(_playerCardX, _playerCardY, _boxHeight - _diceSize - _floorSize * 2));
            playerBoardModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(80, 15, 40));
            playerBoardModel.VerticalCutouts.Right = new HorizontalCutout(new Vector3(80, 15, 40), 4);
            box.Components.Add(new BoxComponent(playerBoardModel), new ComponentLocation(new Vector3()));

            var diceComponentModel = new BoxComponentDataModel(new Vector3(_diceSize * 2, _diceSize * 4, _diceSize));
            diceComponentModel.HorizontalCutouts.Top = new CutoutCube(new Vector3(30, 20, 25));
            diceComponentModel.HorizontalCutouts.Bottom = new CutoutCube(new Vector3(30, 20, 25));

            var fameMarker = new ImageComponent("FameMarker.svg", 15)
            {
                Size = new Vector3(15, 29, 13)
            };

            var destructionMarker = new ImageComponent("DestructionMarker.svg", 15)
            {
                Size = new Vector3(15, 26, 13)
            };

            box.Components.Add(new BoxComponent(diceComponentModel), new ComponentLocation(new Vector3(-25, 0, -playerBoardHeight)));

            var markerCutout1 = new BoxComponent(new BoxComponentDataModel(new Vector3(45, 80, 20)));

            box.Components.Add(fameMarker, new ComponentLocation(new Vector3(0, -26, -playerBoardHeight), new Vector3(0, 0, 90), scale: new Vector3(1.025, 1.125, 1)));
            box.Components.Add(destructionMarker, new ComponentLocation(new Vector3(3, 38, -playerBoardHeight), new Vector3(0, 0, 90), scale: new Vector3(1.025, 1.1, 1)));
            box.Components.Add(markerCutout1, new ComponentLocation(new Vector3(50, 0, -playerBoardHeight)));

            var diceTokenBoxModel = new BoxComponentDataModel(new Vector3(_diceTokenSize, _diceTokenSize, _diceTokenHeight));
            diceTokenBoxModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(_diceTokenSize - 4, 20, _diceTokenHeight + 20));
            var diceTokenBox = new BoxComponent(diceTokenBoxModel);

            box.Components.Add(diceTokenBox, new ComponentLocation(new Vector3(10, 0, -playerBoardHeight)));

            /*var cardCutout = new BoxComponentDataModel(new Vector3(15, 26, box.Size.Z));
            box.Components.Add(new BoxComponent(cardCutout), new ComponentLocation(new Vector3((box.Size.X - cardCutout.Size.X) / 2, -41)));*/

            return box;
        }

        private OSCADObject RenderCardTray(double xSize)
        {
            var size = new Vector3(xSize, Constants.CardSizeSleevedX + _wallSize * 2, _boxHeight);

            if (_boxHeight < Constants.CardSizeSleevedY * _cards + _floorSize)
            {
                throw new Exception("Cards don't fit");
            }

            var cardHolder = new CardHolder(new CardHolderModel(size, new Vector3(Constants.CardSizeSleevedZ, Constants.CardSizeSleevedX, _boxHeight - _floorSize)));
            return cardHolder;
        }

        private OSCADObject RenderPlayerBoard(double height)
        {
            var box = new InsertBox(new Vector3(_playerCardX + _wallSize * 2, _boxDepth, height));
            var playerBoardModel = new BoxComponentDataModel(new Vector3(_playerCardX, _playerCardY, height - _floorSize));
            playerBoardModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(80, 15, 40));
            playerBoardModel.VerticalCutouts.Right = new HorizontalCutout(new Vector3(80, 15, 40), 4);
            box.Components.Add(new BoxComponent(playerBoardModel), new ComponentLocation(new Vector3()));

            return box;
        }

        private OSCADObject RenderTokenBox1(double x, double y, double height)
        {
            var box = new InsertBox(new Vector3(x, y, height));
            var insideModel = new BoxComponentDataModel(new Vector3(x - _wallSize * 2, y - _wallSize * 2, height - _floorSize));
            insideModel.RoundBottom = true;

            box.Components.Add(new BoxComponent(insideModel), new ComponentLocation(new Vector3()));
            return box;
        }

        private OSCADObject RenderTokenBox2(double x, double y, double height)
        {
            var box = new InsertBox(new Vector3(x, y, height));
            var insideModel = new BoxComponentDataModel(new Vector3(x - _wallSize * 2, 55, height - _floorSize));
            insideModel.RoundBottom = true;

            box.Components.Add(new BoxComponent(insideModel), new ComponentLocation(new Vector3()));
            box.Components.Add(new CircleComponent(new CircleComponentDataModel(new Vector3(_lifeCounterSize, _lifeCounterSize, 11))), new ComponentLocation(new Vector3()));
            return box;
        }

        private OSCADObject TestMarkers()
        {
            var box = new InsertBox(new Vector3(20, 35, 15));

            var fameMarker = new ImageComponent("FameMarker.svg", 15)
            {
                Size = new Vector3(15, 29, 13)
            };

            var destructionMarker = new ImageComponent("DestructionMarker.svg", 15)
            {
                Size = new Vector3(15, 26, 13)
            };

            box.Components.Add(fameMarker, new ComponentLocation(new Vector3(7, 15), scale: new Vector3(1.125, 1.025, 1)));
            //box.Components.Add(destructionMarker, new ComponentLocation(new Vector3(15, 13), scale: new Vector3(1.1, 1.025, 1)));

            return box;
        }
    }
}
