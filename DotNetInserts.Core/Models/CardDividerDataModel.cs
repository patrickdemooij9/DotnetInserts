using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class CardDividerDataModel
    {
        public required Vector3 Size { get; init; }
        public required double WallSize { get; init; }
        public required Vector3 BottomCutoutSize { get; init; }

        public string? ImagePath { get; set; }
    }
}
