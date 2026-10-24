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

namespace DotNetInserts.Projects.Endangered
{
    public class EndangeredBaseGame
    {
        private readonly double _boxSizeX = 287;
        private readonly double _boxSizeY = 213;
        private readonly double _boxSizeZ = 53;

        private readonly int _otterCardAmount = 18;
        private readonly int _tigerCardAmount = 18;
        private readonly int _generalCardAmount = 17;
        private readonly int _characterCardAmount = 42; // Split out the character amount on the base version
        private readonly int _tiles = 24;

        private readonly double _diceSize = 16;
        private readonly double _tilesSize = 41;

        private readonly double _scenarioCardX = 90;
        private readonly double _scenarioCardY = 132;
        private readonly double _scenarioCardZ = 4 * 3;

        private readonly double _wallSize = 2d;
        private readonly double _floorSize = 1.2;

        public void Render()
        {
            var insert = new Insert("EndangeredCoreBox");
            var cardHolderY = _boxSizeY / 3;

            //var topHeight = tiger.Bounds().ZMax * 2 + generalCardsHolder.Bounds().ZMax;
            var topHeight = 38.05;

            var tiger = RenderTigerCardHolder(cardHolderY, topHeight / 2);
            insert.Parts.Add(tiger, new Vector3());

            var otter = RenderOtterCardHolder(cardHolderY, topHeight / 2);
            insert.Parts.Add(otter, new Vector3(z: tiger.Bounds().ZMax));

            var generalCardsHolder = RenderGeneralCardHolder(cardHolderY);
            insert.Parts.Add(generalCardsHolder, new Vector3(y: tiger.Bounds().YMax, z: topHeight - generalCardsHolder.Bounds().ZMax));

            insert.Parts.Add(RenderCharacterCardHolder(cardHolderY, topHeight - _floorSize - generalCardsHolder.Bounds().ZMax, true), new Vector3(y: tiger.Bounds().YMax));
            insert.Parts.Add(RenderCharacterCardHolder(cardHolderY, topHeight - _floorSize, false), new Vector3(y: tiger.Bounds().YMax * 2));

            var characterBox = RenderCharacterHolder();
            insert.Parts.Add(characterBox, new Vector3(x: tiger.Bounds().XMax + Constants.Tolerance));

            var diceBox = RenderDiceHolder(_boxSizeX - tiger.Bounds().XMax - characterBox.Bounds().XMax, (topHeight) / 2);
            insert.Parts.Add(diceBox, new Vector3(tiger.Bounds().XMax + characterBox.Bounds().XMax));

            var tiles = RenderTilesHolder(characterBox.Bounds().XMax, _boxSizeY - characterBox.Bounds().YMax, topHeight);
            insert.Parts.Add(tiles, new Vector3(x: tiger.Bounds().XMax, y: characterBox.Bounds().YMax));

            var cubesAndMoney = RenderCubesAndMoney(_boxSizeX - tiger.Bounds().XMax - characterBox.Bounds().XMax, _boxSizeY - diceBox.Bounds().YMax, (topHeight) / 2);
            insert.Parts.Add(cubesAndMoney, new Vector3(tiger.Bounds().XMax + characterBox.Bounds().XMax, diceBox.Bounds().YMax));

            var animals = RenderAnimals(_boxSizeX - tiger.Bounds().XMax - characterBox.Bounds().XMax, _boxSizeY - diceBox.Bounds().YMax, (topHeight) / 2);
            insert.Parts.Add(animals, new Vector3(tiger.Bounds().XMax + characterBox.Bounds().XMax, diceBox.Bounds().YMax, cubesAndMoney.Bounds().ZMax));

            var scenarioHolder = RenderScenarioHolder(_boxSizeX - animals.Bounds().XMax - tiger.Bounds().XMax, characterBox.Bounds().YMax, topHeight - characterBox.Bounds().ZMax);
            insert.Parts.Add(scenarioHolder, new Vector3(tiger.Bounds().XMax, 0, characterBox.Bounds().ZMax));

            var characterAbilities = RenderCharacterAbilities(diceBox.Bounds().XMax, _boxSizeY - animals.Bounds().YMax, topHeight - diceBox.Bounds().ZMax);
            insert.Parts.Add(characterAbilities, new Vector3(tiger.Bounds().XMax + scenarioHolder.Bounds().XMax, 0, diceBox.Bounds().ZMax));

            insert.Render(out _);
        }

        public OSCADObject RenderTigerCardHolder(double y, double height)
        {
            var innerSize = new Vector3(Constants.CardSizeSleevedZ, Constants.CardSizeSleevedX, height - _floorSize);
            var outerSize = new Vector3(innerSize.X + _wallSize * 2, y, height);
            return new CardHolder(new CardHolderModel(outerSize, innerSize)
            {
                CutoutSide = SideType.RIGHT,
                RoundedCorners = true,
                StackableTop = true,
                Text = "Tigers"
            });
        }

        public OSCADObject RenderOtterCardHolder(double y, double height)
        {
            var innerSize = new Vector3(Constants.CardSizeSleevedZ, Constants.CardSizeSleevedX, height - _floorSize);
            var outerSize = new Vector3(innerSize.X + _wallSize * 2, y, height);
            return new CardHolder(new CardHolderModel(outerSize, innerSize)
            {
                CutoutSide = SideType.RIGHT,
                RoundedCorners = true,
                StackableBottom = true,
                Text = "Otters"
            });
        }

        public OSCADObject RenderGeneralCardHolder(double y)
        {
            var innerSize = new Vector3(Constants.CardSizeSleevedZ, Constants.CardSizeSleevedX, 0.8 * _generalCardAmount);
            var outerSize = new Vector3(innerSize.X + _wallSize * 2, y, innerSize.Z + _floorSize);
            return new CardHolder(new CardHolderModel(outerSize, innerSize)
            {
                CutoutSide = SideType.RIGHT,
                RoundedCorners = true,
                StackableBottom = true,
            });
        }

        public OSCADObject RenderCharacterCardHolder(double y, double height, bool isOnBottom)
        {
            var innerSize = new Vector3(Constants.CardSizeSleevedZ, Constants.CardSizeSleevedX, height);
            var outerSize = new Vector3(innerSize.X + _wallSize * 2, y, innerSize.Z + _floorSize);
            return new CardHolder(new CardHolderModel(outerSize, innerSize)
            {
                CutoutSide = SideType.RIGHT,
                RoundedCorners = true,
                Text = "Characters",
                TextSize = 10,
                StackableTop = isOnBottom
            });
        }

        public OSCADObject RenderCharacterHolder()
        {
            var height = 2.5 * 6;
            // Cards are 106x131;
            var boxSize = new Vector3(106, 131, height) + new Vector3(_wallSize * 2, _wallSize * 2, _floorSize);
            var box = new InsertBox(boxSize, 7.5);
            box.Components.Add(new BoxComponent(new BoxComponentDataModel(new Vector3(106, 64, height))), new ComponentLocation(new Vector3(-(box.Bounds().XMax - 106) / 2 + _wallSize, -(box.Bounds().YMax - 64) / 2 + _wallSize, 0)));

            var rightSideBox = new BoxComponentDataModel(new Vector3(28, 68, height));
            rightSideBox.HorizontalCutouts.Right = new CutoutCube(new Vector3(30, 30, 30), roundRadius: 7.5);
            box.Components.Add(new BoxComponent(rightSideBox), new ComponentLocation(new Vector3(-(box.Bounds().XMax - 106) / 2 + _wallSize + 6, -(box.Bounds().YMax - 68) / 2 + _wallSize + 64, 0)));

            var characterTokenSizeX = 30;
            var characterTokenHeight = 2.25 * 6;
            box.Components.Add(new HexagonComponent(new HexagonComponentDataModel(characterTokenSizeX, characterTokenHeight)), new ComponentLocation(new Vector3(-(box.Bounds().XMax - characterTokenSizeX) / 2 + _wallSize + 10, 15, -characterTokenHeight)));
            var cutout = new BoxComponent(new BoxComponentDataModel(new Vector3(30, 20, 15)));
            box.Components.Add(cutout, new ComponentLocation(new Vector3(-(box.Bounds().XMax - characterTokenSizeX) / 2 + _wallSize + 21, 15, 0)));

            var calenderSize = new Vector3(26, 27, 11);
            var calenderDataModel = new BoxComponentDataModel(calenderSize);
            calenderDataModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(20, 35, 20));
            calenderDataModel.HorizontalCutouts.Left = new CutoutCube(new Vector3(20, 20, 20));
            box.Components.Add(new BoxComponent(calenderDataModel), new ComponentLocation(new Vector3(-(box.Bounds().XMax - calenderSize.X) / 2 + _wallSize + 10, 50, 0)));

            return box;
        }

        public OSCADObject RenderDiceHolder(double x, double height)
        {
            var box = new InsertBox(new Vector3(x, 5 * _diceSize + _wallSize * 2, height), 7.5);

            var dicePartModel = new BoxComponentDataModel(new Vector3(_diceSize * 3, _diceSize * 5, _diceSize));
            dicePartModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(30, 35, _diceSize), new Vector3(z: 5));
            dicePartModel.HorizontalCutouts.Left = new CutoutCube(new Vector3(70, 35, _diceSize), new Vector3(z: 5));
            box.Components.Add(new BoxComponent(dicePartModel), new ComponentLocation(new Vector3(-4)));

            var individualDiceModel = new BoxComponentDataModel(new Vector3(_diceSize, _diceSize + .5, _diceSize));
            individualDiceModel.HorizontalCutouts.Top = new CutoutCube(new Vector3(_diceSize, 10, _diceSize), new Vector3(z: 5));
            individualDiceModel.HorizontalCutouts.Bottom = new CutoutCube(new Vector3(_diceSize, 15, _diceSize), new Vector3(z: 5));
            box.Components.Add(new BoxComponent(individualDiceModel), new ComponentLocation(new Vector3((box.Bounds().XMax - _diceSize) / 2 - _wallSize, -(box.Bounds().YMax - _diceSize) / 2 + 10)));

            var individualDiceModel2 = new BoxComponentDataModel(new Vector3(_diceSize, _diceSize + .5, _diceSize));
            individualDiceModel2.HorizontalCutouts.Top = new CutoutCube(new Vector3(_diceSize, 15, _diceSize), new Vector3(z: 5));
            individualDiceModel2.HorizontalCutouts.Bottom = new CutoutCube(new Vector3(_diceSize, 10, _diceSize), new Vector3(z: 5));
            box.Components.Add(new BoxComponent(individualDiceModel2), new ComponentLocation(new Vector3((box.Bounds().XMax - _diceSize) / 2 - _wallSize, (box.Bounds().YMax - _diceSize) / 2 - 10)));

            return box;
        }

        public OSCADObject RenderTilesHolder(double x, double y, double height)
        {
            //var box = new InsertBox(new Vector3(_tilesSize * 2 + _wallSize * 3, y, 2.5 * _tiles / 2 + _floorSize), 7.5);
            var box = new InsertBox(new Vector3(x, y, height), 7.5);

            var tileModel = new BoxComponentDataModel(new Vector3(_tilesSize, _tilesSize, 2.5 * _tiles / 2));
            tileModel.HorizontalCutouts.Bottom = new CutoutCube(new Vector3(_tilesSize - 2, 40, 50));
            box.Components.Add(new BoxComponent(tileModel), new ComponentLocation(new Vector3(-(box.Size.X - _tilesSize) / 2 + 10)));
            box.Components.Add(new BoxComponent(tileModel), new ComponentLocation(new Vector3((box.Size.X - _tilesSize) / 2 - 10)));
            return box;
        }

        public OSCADObject RenderScenarioHolder(double x, double y, double height)
        {
            var box = new InsertBox(new Vector3(x, y, height), 7.5);

            var innerBoxModel = new BoxComponentDataModel(new Vector3(_scenarioCardX, _scenarioCardY, box.Bounds().ZMax - _floorSize));
            innerBoxModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(30, 30, 30));
            innerBoxModel.VerticalCutouts.Right = new HorizontalCutout(new Vector3(30, 30, 20), 8);
            box.Components.Add(new BoxComponent(innerBoxModel), new ComponentLocation(new Vector3()));

            return box;
        }

        public OSCADObject RenderCharacterAbilities(double x, double y, double height)
        {
            var box = new InsertBox(new Vector3(x, y, height), 7.5);

            if (box.Size.Z - _floorSize < Constants.CardSizeSmallSleevedY * 12)
            {
                throw new InvalidOperationException("Won't fit");
            }

            var innerBoxModel = new BoxComponentDataModel(new Vector3(Constants.CardSizeSmallSleevedX, Constants.CardSizeSmallSleevedZ, box.Size.Z - _floorSize));
            innerBoxModel.HorizontalCutouts.Left = new CutoutCube(new Vector3(25, 35, 30));
            innerBoxModel.VerticalCutouts.Left = new HorizontalCutout(new Vector3(25, 35, 30), 8);

            box.Components.Add(new BoxComponent(innerBoxModel), new ComponentLocation(new Vector3()));
            return box;
        }

        public OSCADObject RenderCubesAndMoney(double x, double y, double height)
        {
            var box = new InsertBox(new Vector3(x, y, height), 7.5);

            var innerBox = new BoxComponentDataModel(new Vector3(x - _wallSize * 2, y / 2 - _wallSize * 2, box.Size.Z - _floorSize))
            {
                RoundBottom = true
            };
            box.Components.Add(new BoxComponent(innerBox), new ComponentLocation(new Vector3(y: -(box.Size.Y - innerBox.Size.Y) / 2 + _wallSize, z: .1)));
            box.Components.Add(new BoxComponent(innerBox), new ComponentLocation(new Vector3(y: (box.Size.Y - innerBox.Size.Y) / 2 - _wallSize, z: .1)));

            return box;
        }

        public OSCADObject RenderAnimals(double x, double y, double height)
        {
            var box = new InsertBox(new Vector3(x, y, height), 7.5);

            var innerBox = new BoxComponentDataModel(new Vector3(x - _wallSize * 2, y / 2 - _wallSize * 2, box.Size.Z - _floorSize));
            box.Components.Add(new BoxComponent(innerBox), new ComponentLocation(new Vector3(y: -(box.Size.Y - innerBox.Size.Y) / 2 + _wallSize, z: .1)));
            box.Components.Add(new BoxComponent(innerBox), new ComponentLocation(new Vector3(y: (box.Size.Y - innerBox.Size.Y) / 2 - _wallSize, z: .1)));

            return box;
        }
    }
}
