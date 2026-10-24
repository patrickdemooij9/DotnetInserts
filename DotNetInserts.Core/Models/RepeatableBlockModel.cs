using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class RepeatableBlockModel
    {
        public Vector3 Size { get; set; }
        public int AmountX { get; set; }
        public int AmountY { get; set; }
        public IComponentDataModel Model { get; }
        public double WallSize = 2;

        public RepeatableBlockModel(Vector3 size, int amountX, int amountY, IComponentDataModel model)
        {
            Size = size;
            AmountX = amountX;
            AmountY = amountY;
            Model = model;
        }
    }
}
