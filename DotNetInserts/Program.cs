using DotNetInserts.CustomBlocks;
using DotNetInserts.CustomBlocks.CustomCutouts;
using OSCADSharp.Solids.Compound;
using OSCADSharp.Spatial;
using System;

namespace DotNetInserts
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var wallSize = 3;

            //var cutout = new HorizontalCutout(new Vector3(2, 50, Constants.CardSizeSleevedZ));

            /*var divider = new CardDivider(new Vector3(2, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ), 1.5, new Vector3(2.5, 20, 2));
             var box = new InsertBox(new Vector3(30, Constants.CardSizeSleevedX + 4 * 2, Constants.CardSizeSleevedZ + 6))
             {
                 AddLid = true
             };
             var tray = new CardTrayComponent(new Vector3(30 - 6, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ), divider, 1);
             box.Components.Add(tray, new Vector3());*/

            var divider = new CardDivider(new Vector3(2, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ), 1.5, new Vector3(2.5, 20, 2));
            var box = new InsertBox(new Vector3(250, 150, Constants.CardSizeSleevedZ + 6))
            {
                AddLid = true
            };
            var tray = new CardTrayComponent(new Vector3(250 - 6, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ), divider, 18);
            var secondTray = new CardTrayComponent(new Vector3(250 - 6, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ), divider, 18);
            box.Components.Add(tray, new Vector3(0, box.Size.Y / 2 - 4 - tray.Size.Y / 2, 0));
            box.Components.Add(secondTray, new Vector3(0, -(box.Size.Y / 2 - 4 - tray.Size.Y / 2), 0));
            //box.Cutouts.Top = new CutoutCube(new Vector3(30, 10, 50));

            //var tray = new CardDivider(new Vector3(1, 100, 70), 3, new Vector3(1, 20, 1.2));

            //tray.GetDivider().WithTolerance().ToFile("test.scad");
            //box.GetLid().WithTolerance().ToFile("test.scad");
            box.ToFile("test.scad");

            Console.ReadKey();
        }
    }
}
