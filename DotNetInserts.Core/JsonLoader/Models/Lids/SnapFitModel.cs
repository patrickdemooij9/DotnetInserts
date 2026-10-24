using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;

namespace DotNetInserts.Core.JsonLoader.Models.Lids
{
    /// <summary>
    /// JSON shape of the cantilever snap closure. Plain numbers rather than dimension
    /// objects, same call as roundRadius and the label fields: these are fixed
    /// mechanical sizes, not measurements that relate to other parts.
    /// </summary>
    public class SnapFitModel
    {
        public SideType[]? Sides { get; set; }
        public double? ArmLength { get; set; }
        public double? ArmHeight { get; set; }

        /// <summary>
        /// 0 means the breakaway build: a slot too narrow for the slicer to fit a
        /// perimeter into, so it fuses and gets cracked free by hand. See
        /// <see cref="Helpers.SnapFitHelper.FusedSlotWidth"/>.
        /// </summary>
        public double? SlotWidth { get; set; }

        public double? Detent { get; set; }
        public double? LeadInAngle { get; set; }
        public double? RetentionAngle { get; set; }
        public double? EngageClearance { get; set; }

        public SnapFitDataModel ToDataModel()
        {
            var model = new SnapFitDataModel();

            if (Sides is { Length: > 0 }) model.Sides = [.. Sides];
            if (ArmLength.HasValue) model.ArmLength = ArmLength.Value;
            if (ArmHeight.HasValue) model.ArmHeight = ArmHeight.Value;
            if (SlotWidth.HasValue) model.SlotWidth = SlotWidth.Value;
            if (Detent.HasValue) model.Detent = Detent.Value;
            if (LeadInAngle.HasValue) model.LeadInAngle = LeadInAngle.Value;
            if (RetentionAngle.HasValue) model.RetentionAngle = RetentionAngle.Value;
            if (EngageClearance.HasValue) model.EngageClearance = EngageClearance.Value;

            return model;
        }
    }
}
