namespace DotNetInserts.Core.JsonLoader.Models.Lids
{
    public class LidDataModel
    {
        public double WallSize { get; set; }
        public double TopSize { get; set; }
        public double? LidBoxSize { get; set; }

        /// <summary>
        /// Superseded by <see cref="Grip"/>. Cannot be combined with <see cref="Snap"/>.
        /// </summary>
        public double? SpaceUnderLid { get; set; }

        public string? ImagePath { get; set; }

        // --------------------------------------------------------- explicit fit
        //
        // Naming any of these switches the lid onto the explicit fit model, where the
        // skirt thickness and the clearance are independent numbers. Without them the
        // original behaviour is kept exactly, so existing files do not move.

        public double? SkirtThickness { get; set; }
        public double? Clearance { get; set; }
        public double? MinWall { get; set; }

        public SnapFitModel? Snap { get; set; }
        public LidGripModel? Grip { get; set; }
    }
}
