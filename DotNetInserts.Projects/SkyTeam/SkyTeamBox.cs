using DotNetInserts.Core.CustomBlocks;
using OSCADSharp.Spatial;

namespace DotNetInserts.Projects.SkyTeam
{
    public class SkyTeamBox
    {
        readonly Vector3 _innerSize = new (247, 177, 38);
        readonly double WallSize = 3;

        public void Render()
        {
            var box = new InsertBox(_innerSize);

            var flightPlanSize = new Vector3(_innerSize.X - WallSize * 2, 30, 20);
            /*box.Components.Add(new BoxComponent(flightPlanSize), new Vector3(0, _innerSize.Y / 2 - WallSize - flightPlanSize.Y / 2, 0));*/

            box.ToFile("SkyTeam.scad");
        }
    }
}
