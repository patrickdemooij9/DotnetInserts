using DotNetInserts.Core.CustomBlocks;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    public class InsertBoxLidDataModel
    {
        public double WallSize { get; }
        /// <summary>
        /// Size of the lid sticking out above the actual box
        /// </summary>
        public double TopSize { get; }
        /// <summary>
        /// Height of the lid. Defaults to 50% of the box
        /// </summary>
        public double? LidBoxSize { get; set; }
        /// <summary>
        /// Extra space under the lid on the edges to more easily take the lid off.
        /// Superseded by <see cref="Grip"/>, which is parametric and does not fall apart
        /// on narrow boxes; kept because existing files use it.
        /// </summary>
        public double? SpaceUnderLid { get; set; }
        public string? ImagePath { get; set; }
        public BoxLidPatternModel? PatternData { get; set; }

        // ------------------------------------------------------------- explicit fit

        /// <summary>
        /// Thickness of the lid's skirt wall. Default 1.6 = four perimeters at a 0.4 mm
        /// line width.
        /// </summary>
        public double? SkirtThickness { get; set; }

        /// <summary>Air per side between the skirt and the box's rebate.</summary>
        public double? Clearance { get; set; }

        /// <summary>Skirts thinner than this are rejected rather than emitted.</summary>
        public double MinWall { get; set; } = 0.8;

        public SnapFitDataModel? Snap { get; set; }
        public LidGripDataModel? Grip { get; set; }

        /// <summary>Name of the part this lid belongs to, purely for error messages.</summary>
        public string? OwnerName { get; set; }

        /// <summary>
        /// Whether to use the explicit skirt/clearance model instead of the original
        /// derived one.
        ///
        /// The old path computed the skirt as (WallSize - 1.5)/2 and the clearance as a
        /// fixed 0.75 per side from the same constant. That cannot carry a snap fit, but
        /// changing it unconditionally would move the geometry of every file already
        /// using a lid. So the new model activates only once a file asks for it.
        /// </summary>
        public bool UsesExplicitFit =>
            SkirtThickness.HasValue || Clearance.HasValue || Snap is not null || Grip is not null;

        public LidFit ResolveFit(Vector3 boxSize, double? cornerDiameter)
        {
            if (Snap is not null && SpaceUnderLid.HasValue)
            {
                // spaceUnderLid relieves the rebate to let a friction lid be pulled off.
                // Doing that to a snap lid removes the very material the catch needs, and
                // the two have never been rendered together. Use grip instead.
                throw new InvalidOperationException(
                    $"lid on '{OwnerName ?? "box"}': snap and spaceUnderLid cannot be combined. " +
                    "Use grip, which does the same job parametrically.");
            }

            var fit = new LidFit
            {
                SkirtThickness = SkirtThickness ?? 1.6,
                Clearance = Clearance ?? 0.15,
                LidHeight = LidBoxSize ?? boxSize.Z / 2,
                TopThickness = TopSize,
                BoxSize = boxSize,
                CornerDiameter = cornerDiameter
            };

            fit.Validate(MinWall, OwnerName);
            return fit;
        }

        public InsertBoxLidDataModel(double wallSize, double topSize)
        {
            WallSize = wallSize;
            TopSize = topSize;

        }
    }
}
