using DotNetInserts.Core;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Spatial;

namespace DotNetInserts.Projects.Shatterpoint
{
    public class ShatterpointBox
    {
        private double _boxX = 245;
        private double _boxY = 170;
        private double _floorSize = 1.2;

        private double _range5Length = 255;
        private double _range4Length = 205;
        private double _range3Length = 153;
        private double _range2Length = 78;
        private double _rangeSlowMoveLength = 87;
        private double _rangeFastMoveLength = 157;
        private double _rangeShoveLength = 170;
        private double _rangeY = 26;
        private double _smallRangeY = 16;
        private double _rangeThickness = 6.2;

        public void Render()
        {
            var insert = new Insert("ShatterpointBox");

            insert.Parts.Add(RenderRangeLayer(), new Vector3());

            insert.Render(out _);
        }

        private OSCADObject RenderRangeLayer()
        {
            var box = new InsertBox(new Vector3(_boxX, _boxY, _rangeThickness + _floorSize), 20);
            var angle = 33;

            var range5Comp = new BoxComponentDataModel(new Vector3(_range5Length, _rangeY, _rangeThickness));
            range5Comp.HorizontalCutouts.Right = new CutoutCube(new Vector3(20, 20, _rangeThickness + 10));
            box.Components.Add(range5Comp.GetComponent(), new ComponentLocation(new Vector3(0, -2, 0), new Vector3(0, 0, angle)));

            var cSide = Math.Sqrt(Math.Pow(_boxX, 2) + Math.Pow(_boxY, 2));
            var ratioX = _boxX / cSide + .1;
            var ratioY = _boxY / cSide;

            var differenceAmount = 28.5;
            var difference = 28.5 - 2;
            var range4Comp = new BoxComponentDataModel(new Vector3(_range4Length, _rangeY, _rangeThickness));
            range4Comp.HorizontalCutouts.Right = new CutoutCube(new Vector3(15, 20, _rangeThickness + 10));
            box.Components.Add(range4Comp.GetComponent(), new ComponentLocation(new Vector3(-difference * ratioX, difference * ratioY, 0), new Vector3(0, 0, angle)));

            difference += differenceAmount;
            var range3Comp = new BoxComponentDataModel(new Vector3(_range3Length, _rangeY, _rangeThickness));
            range3Comp.HorizontalCutouts.Right = new CutoutCube(new Vector3(20, 20, _rangeThickness + 10), new Vector3(-(_rangeY - 20) / 2, 0, 0));
            box.Components.Add(range3Comp.GetComponent(), new ComponentLocation(new Vector3(-difference * ratioX, difference * ratioY, 0), new Vector3(0, 0, angle)));

            difference += differenceAmount;
            var range2Comp = new BoxComponentDataModel(new Vector3(_range2Length, _rangeY, _rangeThickness));
            range2Comp.HorizontalCutouts.Right = new CutoutCube(new Vector3(20, 20, _rangeThickness + 10));
            box.Components.Add(range2Comp.GetComponent(), new ComponentLocation(new Vector3(-difference * ratioX, difference * ratioY, 0), new Vector3(0, 0, angle)));

            difference = differenceAmount + 2;
            /*var rangeShoveComp = new BoxComponentDataModel(new Vector3(_rangeShoveLength, _rangeY, _rangeThickness));
            rangeShoveComp.HorizontalCutouts.Right = new CutoutCube(new Vector3(20, 20, _rangeThickness + 10));
            box.Components.Add(rangeShoveComp.GetComponent(), new ComponentLocation(new Vector3(difference * ratioX, -difference * ratioY, 0), new Vector3(0, 0, angle)));*/
            var rangeImg = new ImageComponent("SWDrawer.svg", _rangeThickness) { Size = new Vector3(_rangeShoveLength, _rangeY, _rangeThickness) };
            box.Components.Add(rangeImg, new ComponentLocation(new Vector3(difference * ratioX + 50, -difference * ratioY + 50, 0), new Vector3(0, 0, angle)));

            difference += 23;
            var fastComp = new BoxComponentDataModel(new Vector3(_rangeFastMoveLength, _smallRangeY, _rangeThickness));
            box.Components.Add(fastComp.GetComponent(), new ComponentLocation(new Vector3(difference * ratioX, -difference * ratioY, 0), new Vector3(0, 0, angle)));

            difference += 25;
            var slowComp = new BoxComponentDataModel(new Vector3(_rangeSlowMoveLength, _smallRangeY, _rangeThickness));
            slowComp.HorizontalCutouts.Bottom = new CutoutCube(new Vector3(20, 20, _rangeThickness + 10), new Vector3(0, 12.5, 0));
            box.Components.Add(slowComp.GetComponent(), new ComponentLocation(new Vector3(difference * ratioX, -difference * ratioY, 0), new Vector3(0, 0, angle)));

            return box;
        }
    }
}
