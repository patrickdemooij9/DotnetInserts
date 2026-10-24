using DotNetInserts.Core.CustomBlocks;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class CardTrayDataModel
    {
        public Vector3 Size { get; }
        public CardDivider Divider { get; }
        public int AmountOfDividers { get; }

        public double Angle { get; set; }

        /// <summary>
        /// Thickness of the surrounding part's wall on each side, same contract as
        /// <see cref="BoxComponentDataModel.Walls"/>. The tray's own end scoops are
        /// measured inward from the outer surface, so without this they stop at the
        /// cavity edge and cut nothing. Filled in by InsertBox.
        /// </summary>
        public WallThickness Walls { get; set; } = WallThickness.None;

        public CardTrayDataModel(Vector3 size, CardDivider divider, int amountOfDividers)
        {
            Size = size;
            Divider = divider;
            AmountOfDividers = amountOfDividers;
        }
    }
}
