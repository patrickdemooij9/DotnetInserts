using DotNetInserts.Core;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Projects.Talisman
{
    public class TalismanCoreBox
    {
        //private Vector3 _innerSize = new(253, 253, 42);
        private Vector3 _innerSize = new(257, 257, 43);

        public void Render()
        {
            var insert = new Insert("Talisman");

            var charactersCardHolder = GetCharactersCardHolder();
            insert.Parts.Add(charactersCardHolder, new Vector3(0, 0, 0));

            var miniHolder = GetMinisHolder();
            insert.Parts.Add(miniHolder, new Vector3(0, _innerSize.Y - miniHolder.Bounds().YMax, 0));

            var bigMiniHolder = GetBigMinisHolder(charactersCardHolder.Bounds().XMax, _innerSize.Y - charactersCardHolder.Bounds().YMax - miniHolder.Bounds().YMax - Constants.Tolerance);
            insert.Parts.Add(bigMiniHolder, new Vector3(0, _innerSize.Y - miniHolder.Bounds().YMax - bigMiniHolder.Bounds().YMax, 0));

            var cardHolderX = (_innerSize.X - bigMiniHolder.Bounds().XMax - Constants.Tolerance) / 2;

            var cardHolderEncounter1 = GetCardHolder(cardHolderX, _innerSize.Z - 18);
            var cardHolderEnouncter2 = GetCardHolder(cardHolderX, _innerSize.Z - 18);
            var cardHolderSpells = GetCardHolder(cardHolderX, 18);
            var cardHolderTalisman = GetCardHolder(cardHolderX, 5);
            var cardHolderStore = GetCardHolder(cardHolderX, 18 - 5);

            insert.Parts.Add(cardHolderEncounter1, new Vector3(bigMiniHolder.Bounds().XMax, _innerSize.Y - miniHolder.Bounds().YMax - cardHolderEncounter1.Bounds().YMax, 0));
            insert.Parts.Add(cardHolderEnouncter2, new Vector3(bigMiniHolder.Bounds().XMax + cardHolderEnouncter2.Bounds().XMax, _innerSize.Y - miniHolder.Bounds().YMax - cardHolderEnouncter2.Bounds().YMax, 0));
            insert.Parts.Add(cardHolderSpells, new Vector3(bigMiniHolder.Bounds().XMax, _innerSize.Y - miniHolder.Bounds().YMax - cardHolderEncounter1.Bounds().YMax, cardHolderEncounter1.Bounds().ZMax));
            insert.Parts.Add(cardHolderTalisman, new Vector3(bigMiniHolder.Bounds().XMax + cardHolderEnouncter2.Bounds().XMax, _innerSize.Y - miniHolder.Bounds().YMax - cardHolderEnouncter2.Bounds().YMax, cardHolderEnouncter2.Bounds().ZMax));
            insert.Parts.Add(cardHolderStore, new Vector3(bigMiniHolder.Bounds().XMax + cardHolderEnouncter2.Bounds().XMax, _innerSize.Y - miniHolder.Bounds().YMax - cardHolderEnouncter2.Bounds().YMax, cardHolderEnouncter2.Bounds().ZMax + cardHolderTalisman.Bounds().ZMax));

            var tokenBox1 = GetTokenBox(_innerSize.X - charactersCardHolder.Bounds().XMax - Constants.Tolerance, _innerSize.Y - miniHolder.Bounds().YMax - cardHolderEncounter1.Bounds().YMax - Constants.Tolerance);
            insert.Parts.Add(tokenBox1, new Vector3(charactersCardHolder.Bounds().XMax, 0, 0));

            var tokenBox2 = GetTokenBox2(_innerSize.X - charactersCardHolder.Bounds().XMax - Constants.Tolerance, _innerSize.Y - miniHolder.Bounds().YMax - cardHolderEncounter1.Bounds().YMax - Constants.Tolerance);
            insert.Parts.Add(tokenBox2, new Vector3(charactersCardHolder.Bounds().XMax, 0, tokenBox1.Bounds().Height));

            insert.Render(out _);
        }

        private OSCADObject GetCardHolder(double x, double height)
        {
            return new CardHolder(new CardHolderModel(new Vector3(x, 95, height), new Vector3(60, 91, height - 1.2)));
        }

        private OSCADObject GetCharactersCardHolder()
        {
            var cardSizeX = 105;
            var cardSizeY = 130;

            var box = new InsertBox(new Vector3(cardSizeX + 2 * 2, cardSizeY + 2 * 2, _innerSize.Z));
            var boxComponentDataModel = new BoxComponentDataModel(new Vector3(cardSizeX, cardSizeY, _innerSize.Z - 2));
            boxComponentDataModel.HorizontalCutouts.Right = new CutoutCube(new Vector3(90, 30, 5));
            boxComponentDataModel.HorizontalCutouts.Left = new CutoutCube(new Vector3(90, 30, 5));
            boxComponentDataModel.VerticalCutouts.Right = new HorizontalCutout(new Vector3(90, 4, 100), 4);
            boxComponentDataModel.VerticalCutouts.Left = new HorizontalCutout(new Vector3(90, 4, 100), 4);

            box.Components.Add(new BoxComponent(boxComponentDataModel), new ComponentLocation(new Vector3(0, 0, 0)));
            return box;
        }

        private OSCADObject GetMinisHolder()
        {
            var miniHolders = new InsertBox(new Vector3(_innerSize.X, 55, _innerSize.Z));
            var miniHoldersComp = new MiniHolderComponent(new MiniHolderComponentDataModel(26, 5, 40));

            // There is probably a better way to calculate this, but I am done with it
            var wallSize = (_innerSize.X - ((-_innerSize.X / 2 + .5 * miniHoldersComp.Model.BaseRadius + 9 * miniHoldersComp.Model.BaseRadius / 1.1) - (-_innerSize.X / 2 + .5 * miniHoldersComp.Model.BaseRadius + 0 * miniHoldersComp.Model.BaseRadius / 1.1) + 26)) / 2;
            for (var i = 0; i < 10; i++)
            {
                var x = -_innerSize.X / 2 + wallSize + .5 * miniHoldersComp.Model.BaseRadius + i * miniHoldersComp.Model.BaseRadius / 1.1;
                var y = -3.5;
                Vector3? rotation = null;
                if (i % 2 != 0)
                {
                    rotation = new Vector3(0, 0, 180);
                    y = 3.5;
                }

                miniHolders.Components.Add(miniHoldersComp.Clone() as IComponent, new ComponentLocation(new Vector3(x, y, -5), rotation));
            }

            miniHolders.Components.Add(new BoxComponent(new BoxComponentDataModel(new Vector3(_innerSize.X - wallSize * 2, 55, 5))), new ComponentLocation(new Vector3(0, 0, 0)));
            miniHolders.Components.Add(new BoxComponent(new BoxComponentDataModel(new Vector3(_innerSize.X - wallSize * 2, 40, 10))), new ComponentLocation(new Vector3(0, 0, -_innerSize.Z + 10 + 2)));

            return miniHolders;
        }

        private OSCADObject GetBigMinisHolder(double x, double y)
        {
            var insertBox = new InsertBox(new Vector3(x, y, _innerSize.Z));
            insertBox.Components.Add(new MiniHolderComponent(new MiniHolderComponentDataModel(26, 5, 55)), new ComponentLocation(new Vector3(x / 5, 0, -5)));
            insertBox.Components.Add(new MiniHolderComponent(new MiniHolderComponentDataModel(31, 5, 55)), new ComponentLocation(new Vector3(-x / 5, 0, -5)));

            insertBox.Components.Add(new BoxComponent(new BoxComponentDataModel(new Vector3(insertBox.Size.X - 2 * 2, 55 - 5, 41))), new ComponentLocation(new Vector3(0, 1, 0)));
            insertBox.Components.Add(new BoxComponent(new BoxComponentDataModel(new Vector3(insertBox.Size.X - 2 * 2, insertBox.Size.Y, 5))), new ComponentLocation(new Vector3()));

            return insertBox;
        }

        private OSCADObject GetTokenBox(double x, double y)
        {
            /*var insertBox = new InsertBox(new Vector3(x, y, _innerSize.Z));
            var dataModel = new BoxComponentDataModel(new Vector3(x - 2 * 2, y - 2 * 2, _innerSize.Z - 2))
            {
                RoundBottom = true
            };
            var repeatableBlockModel = new RepeatableBlockModel(dataModel.Size, 1, 3, dataModel);

            insertBox.Components.Add(new RepeatableBlock(repeatableBlockModel), new ComponentLocation(new Vector3()));
            insertBox.Components.Add(new BoxComponent(new BoxComponentDataModel(new Vector3(x - 2 * 2, y, 15))), new ComponentLocation(new Vector3()));
            return insertBox;*/

            var insertBox = new InsertBox(new Vector3(x, y, 18.2));
            var insideSizeWithoutWalls = new Vector3(x - 2 * 2, y - 2 * 2, 0);

            var fateTokensDataModel = new BoxComponentDataModel(new Vector3(insideSizeWithoutWalls.X, insideSizeWithoutWalls.Y / 3 * 2 - 2, insertBox.Size.Z - 1.2));

            insertBox.Components.Add(new BoxComponent(fateTokensDataModel), new ComponentLocation(new Vector3(0, (insideSizeWithoutWalls.Y - fateTokensDataModel.Size.Y) / 2, 0)));

            var dataModelTokens = new BoxComponentDataModel(new Vector3(x - 2 * 2, insideSizeWithoutWalls.Y / 3 - 2, insertBox.Size.Z - 1.2));
            var repeatableBlockModel = new RepeatableBlockModel(dataModelTokens.Size, 4, 1, dataModelTokens);

            insertBox.Components.Add(new RepeatableBlock(repeatableBlockModel), new ComponentLocation(new Vector3(0, -(insideSizeWithoutWalls.Y - repeatableBlockModel.Size.Y) / 2)));
            insertBox.Components.Add(new BoxComponent(new BoxComponentDataModel(new Vector3(x, 20, 5))), new ComponentLocation(new Vector3(0, -(insideSizeWithoutWalls.Y - repeatableBlockModel.Size.Y) / 2, 0)));

            return insertBox;
        }

        private OSCADObject GetTokenBox2(double x, double y)
        {
            var coinBoxX = 30;
            var bigConesY = (y - 3 * 2) / 3;

            var insertBox = new InsertBox(new Vector3(x, y, _innerSize.Z - 18.2));
            var dataModel = new BoxComponentDataModel(new Vector3(x - coinBoxX - 2 * 3, (y - 3 * 2) - bigConesY, insertBox.Size.Z - 1.2))
            {
                RoundBottom = true
            };
            var repeatableBlockModel = new RepeatableBlockModel(dataModel.Size, 3, 1, dataModel);

            var bigConesDataModel = new BoxComponentDataModel(new Vector3(x - coinBoxX - 2 * 3, bigConesY, insertBox.Size.Z - 1.2))
            {
                RoundBottom = true
            };
            var repeatableConesBlockModel = new RepeatableBlockModel(bigConesDataModel.Size, 3, 1, bigConesDataModel);

            insertBox.Components.Add(new RepeatableBlock(repeatableBlockModel), new ComponentLocation(new Vector3(-coinBoxX / 2 - 1, -bigConesY / 2 - 1, 0)));
            insertBox.Components.Add(new RepeatableBlock(repeatableConesBlockModel), new ComponentLocation(new Vector3(-coinBoxX / 2 - 1, bigConesY + 1, 0)));
            insertBox.Components.Add(new BoxComponent(new BoxComponentDataModel(new Vector3(coinBoxX, y - 2 * 2, insertBox.Size.Z - 1.2))
            {
                RoundBottom = true
            }), new ComponentLocation(new Vector3(x / 2 - coinBoxX / 2 - 2, 0, 0)));

            return insertBox;
        }
    }
}
