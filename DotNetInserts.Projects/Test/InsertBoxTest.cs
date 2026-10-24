using DotNetInserts.Core;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Models;
using OSCADSharp.Spatial;

namespace DotNetInserts.Projects.Test
{
    public class InsertBoxTest
    {
        public void Render()
        {
            /*var divider = new CardDivider(new CardDividerDataModel
            {
                Size = new Vector3(2, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ),
                WallSize = 1.5,
                BottomCutoutSize = new Vector3(2.5, 20, 2)
            });
            var box = new InsertBox(new Vector3(30, Constants.CardSizeSleevedX + 4 * 2, Constants.CardSizeSleevedZ + 6))
            {
                Lid = new InsertBoxLidDataModel(3, 3)
            };
            var tray = new CardTrayComponent(new CardTrayDataModel(new Vector3(30 - 6, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ), divider, 1));
            box.Components.Add(tray, new ComponentLocation(new Vector3()));*/

            var insert = new Insert("Test");
            var box = new InsertBox(new Vector3(30, 20, 20))
            {
                Lid = new InsertBoxLidDataModel(3, 1.6)
                {
                    LidBoxSize = 5,
                    PatternData = new BoxLidPatternModel(new Vector3(15, 8, 4))
                    {
                        Radius = 3
                    },
                    SpaceUnderLid = 2
                },
            };
            box.Components.Add(new BoxComponent(new BoxComponentDataModel(new Vector3(25, 15, 18))), new ComponentLocation(new Vector3()));

            insert.Parts.Add(box, new Vector3());
            insert.Parts.Add(box.GetLid().WithTolerance(), new Vector3(0, 0, box.Bounds().ZMax - box.GetLid().Bounds().ZMax));

            //tray.GetDivider().WithTolerance().ToFile("test.scad");
            //box.GetLid().WithTolerance().ToFile("test.scad");
            insert.Render(out _);
        }
    }
}
