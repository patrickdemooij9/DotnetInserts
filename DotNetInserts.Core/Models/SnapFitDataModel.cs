using DotNetInserts.Core.Enums;

namespace DotNetInserts.Core.Models
{
    /// <summary>
    /// A cantilever snap-fit closure: a flexible tongue cut into the lid's skirt with a
    /// detent bump near its free end, clicking into a matching pocket in the box.
    ///
    /// The arms run HORIZONTALLY along the wall, not vertically. The lid prints with its
    /// top plate on the bed and the skirt rising in Z, so a vertical arm would bend
    /// across the layer lines, in the direction the print is weakest. A horizontal tongue
    /// bends in-plane instead.
    /// </summary>
    public class SnapFitDataModel
    {
        /// <summary>
        /// Which walls carry an arm. Two opposing arms give symmetric retention and let
        /// the lid be squeezed to release.
        /// </summary>
        public List<SideType> Sides { get; set; } = [SideType.LEFT, SideType.RIGHT];

        /// <summary>Length of the tongue along the wall.</summary>
        public double ArmLength { get; set; } = 18;

        /// <summary>Height of the tongue.</summary>
        public double ArmHeight { get; set; } = 5;

        /// <summary>
        /// Width of the slot that frees the arm from the surrounding skirt.
        ///
        /// Zero is the "breakaway" build: the walls end up closer together than the
        /// slicer can resolve, so they fuse and the arm is snapped free by hand before
        /// first use. That gives a stiffer arm and no visible gap, but it depends on the
        /// layer height and nozzle it was tuned for, and someone who does not know to
        /// crack it will think the lid is solid.
        ///
        /// The default is a real modelled void, which prints the same everywhere.
        /// </summary>
        public double SlotWidth { get; set; } = 0.6;

        /// <summary>How far the bump stands proud of the arm - the interference.</summary>
        public double Detent { get; set; } = 0.5;

        /// <summary>
        /// Angle from vertical of the face the box meets first as the lid goes on.
        /// Shallow, so the lid pushes on easily.
        /// </summary>
        public double LeadInAngle { get; set; } = 30;

        /// <summary>
        /// Angle from vertical of the holding face. At 45 it is right on the printable
        /// overhang limit on both parts; steeper holds harder but needs support.
        /// </summary>
        public double RetentionAngle { get; set; } = 45;

        /// <summary>Extra depth cut into the catch pocket so the bump seats without binding.</summary>
        public double EngageClearance { get; set; } = 0.1;
    }
}
