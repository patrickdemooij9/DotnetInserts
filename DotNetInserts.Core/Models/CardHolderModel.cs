using DotNetInserts.Core.Enums;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class CardHolderModel
    {
        public Vector3 Size { get; }
        public Vector3 InnerSize { get; }
        public SideType CutoutSide { get; set; } = SideType.BOTTOM;
        public bool RoundedCorners { get; set; }

        /// <summary>
        /// The original single label, engraved into the floor of the well. Kept so files
        /// and projects written against it keep working; <see cref="Labels"/> is the
        /// general form and can put text on a wall.
        /// </summary>
        public string? Text { get; set; }
        public int TextSize { get; set; } = 16;

        public List<LabelDataModel> Labels { get; } = [];

        public bool StackableBottom { get; set; }
        public bool StackableTop { get; set; }

        public CardHolderModel(Vector3 size, Vector3 innerSize)
        {
            Size = size;
            InnerSize = innerSize;
        }

        public CardHolderModel(Vector3 cardSize, int wallSize, int floorSize)
        {
            Size = cardSize + new Vector3(wallSize * 2, wallSize * 2, floorSize);
            InnerSize = cardSize;
        }
    }
}
