using DotNetInserts.Core.Enums;
using DotNetInserts.Core.JsonLoader.Models.Dimensions;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Models.Parts
{
    public class CardHolderPartJsonModel : IPart, IExposeVariables
    {
        public const string Type = "cardHolder";

        public required string Id { get; set; }
        public required string Name { get; set; }
        public bool Hidden { get; set; }
        public required PartDimension Size { get; set; }
        public required PartDimension InnerSize { get; set; }
        public SideType CutoutSide { get; set; } = SideType.BOTTOM;
        public bool RoundedCorners { get; set; }
        /// <summary>
        /// The original single label. It engraves the floor of the well; use
        /// <see cref="Labels"/> to put text on a wall instead, or as well.
        /// </summary>
        public string? Text { get; set; }
        public int TextSize { get; set; } = 16;

        public LabelModel[] Labels { get; set; } = [];

        public bool StackableBottom { get; set; }
        public bool StackableTop { get; set; }

        public PartDimension Location { get; set; } = PartDimension.ZeroDimension();

        public Dictionary<string, IDimension> GetVariables(JsonInsertContext context)
        {
            var variables = new Dictionary<string, IDimension>
            {
                { $"{Name}.X", Size.X },
                { $"{Name}.Y", Size.Y },
                { $"{Name}.Z", Size.Z }
            };
            return variables;
        }
    }
}
