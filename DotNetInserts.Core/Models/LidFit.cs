using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Models
{
    /// <summary>
    /// The resolved millimetre geometry of a lid joint.
    ///
    /// It exists as its own type so the numbers can be asserted directly. The property
    /// that matters - that the air between the lid skirt and the box rebate is exactly
    /// the requested clearance, whatever the wall thickness - is invisible in the emitted
    /// OpenSCAD, and the older path got it structurally wrong: it derived both the skirt
    /// thickness and the clearance from one constant, so asking for a thicker wall
    /// silently changed the fit, and a thin wall produced a skirt too thin to print.
    /// </summary>
    public sealed record LidFit
    {
        public required double SkirtThickness { get; init; }
        public required double Clearance { get; init; }

        /// <summary>Total height of the printed lid, top plate included.</summary>
        public required double LidHeight { get; init; }

        /// <summary>Thickness of the lid's solid top plate.</summary>
        public required double TopThickness { get; init; }

        public required Vector3 BoxSize { get; init; }

        /// <summary>
        /// Corner size of the box, if it has one. Named "diameter" deliberately: the
        /// value flows into <c>Cylinder(diameter, ...)</c>, so a part declaring
        /// <c>roundRadius: 8</c> actually gets a 4 mm radius. The lid has to use the same
        /// number to match the body it sits on.
        /// </summary>
        public double? CornerDiameter { get; init; }

        /// <summary>Taken off the box's outer wall per side, over the skirt's depth.</summary>
        public double Rebate => SkirtThickness + Clearance;

        /// <summary>How far the skirt reaches down the outside of the box.</summary>
        public double SkirtDepth => LidHeight - TopThickness;

        public double SkirtInnerX => BoxSize.X - 2 * SkirtThickness;
        public double SkirtInnerY => BoxSize.Y - 2 * SkirtThickness;
        public double RebateOuterX => BoxSize.X - 2 * Rebate;
        public double RebateOuterY => BoxSize.Y - 2 * Rebate;

        /// <summary>
        /// The air actually left between the two parts, derived from the finished
        /// dimensions rather than from the inputs. Equals <see cref="Clearance"/> when
        /// the geometry is right, which is what makes it worth asserting.
        /// </summary>
        public double ClearancePerSide => (SkirtInnerX - RebateOuterX) / 2;

        /// <summary>Corner size of the lid's own outer body, so it matches the box.</summary>
        public double? LidCornerDiameter => CornerDiameter;

        /// <summary>Corner size of the rebated section, shrunk by what was taken off it.</summary>
        public double? RebateCornerDiameter => Shrink(2 * Rebate);

        /// <summary>Corner size of the lid's inner cavity.</summary>
        public double? SkirtCornerDiameter => Shrink(2 * SkirtThickness);

        private double? Shrink(double by) =>
            CornerDiameter is null ? null : Math.Max(CornerDiameter.Value - by, 0);

        public void Validate(double minWall, string? owner = null)
        {
            var who = string.IsNullOrWhiteSpace(owner) ? "lid" : $"lid on '{owner}'";

            if (SkirtThickness < minWall)
            {
                throw new InvalidOperationException(
                    $"{who}: skirt is {SkirtThickness:0.##} mm, below the {minWall:0.##} mm minimum. " +
                    "Raise lid.skirtThickness, or lower lid.minWall if you really mean it.");
            }

            if (SkirtDepth <= 0)
            {
                throw new InvalidOperationException(
                    $"{who}: lidBoxSize ({LidHeight:0.##}) must exceed topSize ({TopThickness:0.##}), " +
                    "otherwise there is no skirt to grip the box.");
            }

            if (RebateOuterX <= 0 || RebateOuterY <= 0)
            {
                throw new InvalidOperationException(
                    $"{who}: a {Rebate:0.##} mm rebate per side leaves nothing of a " +
                    $"{BoxSize.X:0.##} x {BoxSize.Y:0.##} box.");
            }
        }
    }
}
