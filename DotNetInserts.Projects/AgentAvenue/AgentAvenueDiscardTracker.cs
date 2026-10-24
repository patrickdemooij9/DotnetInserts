using DotNetInserts.Core.CustomBlocks;
using OSCADSharp;
using OSCADSharp.Solids;

namespace DotNetInserts.Projects.AgentAvenue
{
    public class AgentAvenueDiscardTracker
    {
        private double _radius = 10;

        private List<OSCADObject> _characters = new List<OSCADObject>();

        public void Render()
        {
            var tracker = new Cylinder(_radius, 3).Translate(_radius / 2, _radius / 2, 0).Hull(
                new Cylinder(_radius, 3).Translate(85 - _radius / 2, _radius / 2, 0),
                new Cylinder(_radius, 3).Translate(_radius / 2, 55 - _radius / 2, 0),
                new Cylinder(_radius, 3).Translate(85 - _radius / 2, 55 - _radius / 2, 0));

            var discardText = new Text3D("Discard")
            {
                Font = "Arial BOLD",
                Size = 8
            }.Rotate(0, 0, 90).Translate(10, 28, 2);
            _characters.Add(discardText);

            var trackerPoints = RenderChars().Select(it => it.Translate(25, 20, 1)).ToArray();
            _characters.AddRange(trackerPoints);
            var otherSideTrackerPoints = RenderChars().Select(it => it.Rotate(0, 0, 180).Translate(72, 35, 1)).ToArray();
            _characters.AddRange(otherSideTrackerPoints);

            tracker = tracker.Difference(trackerPoints)
                        .Difference(otherSideTrackerPoints)
                        .Difference(RenderPoints().Select(it => it.Translate(25, 20, 1)).ToArray())
                        .Difference(RenderPoints().Select(it => it.Rotate(0, 0, 180).Translate(72, 35, 1)).ToArray())
                        .Difference(discardText);

            tracker.ToFile("AgentAvenueDiscardTracker");

            _characters[0].Union([.._characters[1..]]).ToFile("AgentAvenueDiscardTrackerChars");

            new Cylinder(9.5, 5) { Resolution = 50}.ToFile("AgentAvenueDiscardTrackerDisk");
        }

        private OSCADObject[] RenderPoints()
        {
            var points = new List<OSCADObject>();
            for (var i = 0; i < 5; i++)
            {
                points.Add(new Cylinder(10, 2)
                {
                    Resolution = 50
                }.Translate(i * 12, -12.5, 0));
            }
            return points.ToArray();
        }

        private OSCADObject[] RenderChars()
        {
            var points = new List<OSCADObject>();
            for (var i = 0; i < 5; i++)
            {
                points.Add(new Text3D(i.ToString())
                {
                    Font = "Arial BOLD"
                }.Translate(i * 12, 0, 1));
            }
            return points.ToArray();
        }
    }
}
