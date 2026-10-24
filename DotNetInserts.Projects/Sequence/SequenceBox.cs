using DotNetInserts.Core;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Models;
using OSCADSharp.Spatial;

namespace DotNetInserts.Projects.Sequence
{
    public class SequenceBox
    {
        private int BoxSizeX = 250;
        private int BoxSizeY = 200;
        private int BoxSizeZ = 40;

        private int WallSize = 2;
        private double FloorSize = 1.2;

        public void Render()
        {
            var insert = new Insert("Sequence");

            var cardHolder = new InsertBox(new Vector3(Constants.CardSizeSleevedX + 2 * 2, BoxSizeY, BoxSizeZ));
            var boxDataModel = new BoxComponentDataModel(new Vector3(Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ, BoxSizeZ - FloorSize));
            boxDataModel.HorizontalCutouts.Top = new CutoutCube(new Vector3(40, 60, BoxSizeZ + 1), new Vector3(0, 20, 0));
            boxDataModel.HorizontalCutouts.Bottom = new CutoutCube(new Vector3(40, 60, BoxSizeZ + 1), new Vector3(0, -20, 0));
            cardHolder.Components.Add(new BoxComponent(boxDataModel), new ComponentLocation(new Vector3()));

            var tokenHolder = new InsertBox(new Vector3(BoxSizeX - cardHolder.Bounds().XMax, BoxSizeY / 3 - .1, BoxSizeZ));
            var tokenHolderDataModel = new BoxComponentDataModel(new Vector3(tokenHolder.Bounds().XMax - WallSize * 2, tokenHolder.Bounds().YMax - WallSize * 2, BoxSizeZ - FloorSize + .1))
            {
                RoundBottom = true
            };
            tokenHolder.Components.Add(new BoxComponent(tokenHolderDataModel), new ComponentLocation(new Vector3()));

            insert.Parts.Add(cardHolder, new Vector3());
            insert.Parts.Add(tokenHolder, new Vector3(cardHolder.Bounds().XMax));
            insert.Parts.Add(tokenHolder.Clone(), new Vector3(cardHolder.Bounds().XMax, BoxSizeY / 3));
            insert.Parts.Add(tokenHolder.Clone(), new Vector3(cardHolder.Bounds().XMax, BoxSizeY / 3 * 2));

            insert.Render(out _);
        }
    }
}
