using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;

namespace DotNetInserts.Core.JsonLoader.Models.Lids
{
    /// <summary>JSON shape of the thumb scallop that lets a snapped lid be opened.</summary>
    public class LidGripModel
    {
        public SideType[]? Sides { get; set; }
        public double? Width { get; set; }
        public double? Depth { get; set; }

        public LidGripDataModel ToDataModel()
        {
            var model = new LidGripDataModel();

            if (Sides is { Length: > 0 }) model.Sides = [.. Sides];
            if (Width.HasValue) model.Width = Width.Value;
            if (Depth.HasValue) model.Depth = Depth.Value;

            return model;
        }
    }
}
