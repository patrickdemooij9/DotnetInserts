using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.Helpers
{
    /// <summary>
    /// Geometry for the cantilever snap-fit lid.
    ///
    /// PRINT ORIENTATION drives the whole design. The lid prints top-plate-down, so its
    /// local +Z points downwards on the bed and a surface facing local +Z is an overhang.
    /// The box prints upright, so on that part it is local -Z that overhangs. The two
    /// mating faces of the joint are therefore constrained from opposite directions,
    /// which is why BOTH angles are capped at 45 degrees from vertical - satisfying one
    /// part's overhang limit would otherwise break the other's.
    ///
    /// The arms run along the wall rather than up it for the same reason: the lid's
    /// layers stack along its local Z, so a vertical arm would be bent across the layer
    /// lines, in the direction the print is weakest.
    ///
    /// Everything is built from Cube, Cylinder and Hull. Polygon + LinearExtrude would
    /// express the wedge more directly, but this OSCADSharp fork writes the extrude
    /// height with `$"height={Height}"` - raw interpolation, no invariant culture - so on
    /// a machine with a comma decimal separator `linear_extrude(height=14.6)` comes out
    /// as `linear_extrude(height=14,6)` and OpenSCAD silently reads it as two arguments.
    /// Cube and Cylinder go through StatementBuilder, which does force invariant culture.
    /// </summary>
    public static class SnapFitHelper
    {
        /// <summary>Overshoot so cuts break the surface instead of sitting coplanar with it.</summary>
        private const double Eps = 0.05;

        /// <summary>
        /// Flat at the tip of the detent. A knife-edge apex is fragile and prints badly;
        /// a fifth of a millimetre of flat gives the slicer something to lay down.
        /// </summary>
        public const double TipFlat = 0.2;

        /// <summary>
        /// What <c>slotWidth: 0</c> actually emits.
        ///
        /// A genuinely zero-width slot is not a breakaway, it is no slot at all - the
        /// model would come out solid with no arm in it. The reference design's trick is
        /// a slot NARROWER than one extrusion width, so the slicer cannot fit a perimeter
        /// into it and the two faces fuse; the arm is then cracked free by hand. 0.15 mm
        /// is comfortably under a 0.4 mm nozzle's line width, so it fuses, while still
        /// being a real void in the model.
        /// </summary>
        public const double FusedSlotWidth = 0.15;

        /// <summary>The widest slot that will still fuse rather than print as a gap.</summary>
        public const double FusedSlotCeiling = 0.3;

        public static double EffectiveSlotWidth(SnapFitDataModel snap) =>
            snap.SlotWidth <= 0 ? FusedSlotWidth : snap.SlotWidth;

        // ============================================================== profile

        /// <summary>
        /// The detent's cross-section in the plane normal to the wall: x runs inward from
        /// the face it stands on, y is vertical. Four points - up the lead-in face, across
        /// the tip flat, back down the retention face.
        ///
        /// Exposed because the thing worth checking about this shape is its angles, and
        /// those are far easier to assert here than in the emitted OpenSCAD.
        /// </summary>
        public static Vector2[] DetentProfile(SnapFitDataModel snap, double centreZ, double depth)
        {
            var lead = Rise(depth, snap.LeadInAngle);
            var retain = Rise(depth, snap.RetentionAngle);
            var bottom = centreZ - (lead + TipFlat + retain) / 2;

            return
            [
                new Vector2(0, bottom),
                new Vector2(depth, bottom + lead),
                new Vector2(depth, bottom + lead + TipFlat),
                new Vector2(0, bottom + lead + TipFlat + retain)
            ];
        }

        /// <summary>
        /// How far a face climbs while standing <paramref name="depth"/> proud, for a
        /// given angle off vertical. 45 degrees climbs exactly its own depth; shallower
        /// angles climb further.
        /// </summary>
        public static double Rise(double depth, double angleFromVertical)
        {
            var a = Math.Clamp(angleFromVertical, 1, 89);
            return depth / Math.Tan(a * Math.PI / 180);
        }

        // ================================================================= arms

        /// <summary>The C-shaped slot that frees each arm. Cut from the lid.</summary>
        public static List<OSCADObject> ArmSlots(SnapFitDataModel snap, LidFit fit)
        {
            var slot = EffectiveSlotWidth(snap);
            var result = new List<OSCADObject>();

            foreach (var side in snap.Sides)
            {
                var wall = WallLength(side, fit);
                var armStart = wall / 2 - snap.ArmLength / 2;
                var z0 = ArmBottom(snap, fit);
                var face = SkirtFace(side, fit);

                // Cuts right through the skirt: outward past its face, and a shaved
                // amount inward. The detent is unioned on afterwards, so nicking the
                // inner face here cannot eat into it.
                OSCADObject Bar(double zLo, double zHi, double length) =>
                    new Cube(fit.SkirtThickness + 2 * Eps, zHi - zLo, length)
                        .Translate(-(fit.SkirtThickness + Eps), zLo, 0);

                // above the arm, below it, and around the free end - leaving the tongue
                // rooted at the other end.
                var span = snap.ArmLength + slot;
                result.Add(Place(Bar(z0 + snap.ArmHeight, z0 + snap.ArmHeight + slot, span),
                                 span, side, face, armStart));
                result.Add(Place(Bar(z0 - slot, z0, span),
                                 span, side, face, armStart));
                result.Add(Place(Bar(z0 - slot, z0 + snap.ArmHeight + slot, slot),
                                 slot, side, face, armStart + snap.ArmLength));
            }

            return result;
        }

        /// <summary>The bump on each arm. Added to the lid.</summary>
        public static List<OSCADObject> Detents(SnapFitDataModel snap, LidFit fit)
        {
            var result = new List<OSCADObject>();

            foreach (var side in snap.Sides)
            {
                var (start, length) = DetentSpan(snap, WallLength(side, fit));
                var profile = DetentProfile(snap, DetentCentreZ(snap, fit), snap.Detent);

                result.Add(Place(Wedge(profile, length), length, side, SkirtFace(side, fit), start));
            }

            return result;
        }

        /// <summary>The pocket each bump drops into. Unioned into the box's negative.</summary>
        public static List<OSCADObject> Catches(SnapFitDataModel snap, LidFit fit)
        {
            var result = new List<OSCADObject>();

            foreach (var side in snap.Sides)
            {
                var (start, length) = DetentSpan(snap, WallLength(side, fit));

                // Deeper than the bump so it seats instead of bottoming out, and longer
                // so the two do not have to line up perfectly.
                var profile = DetentProfile(
                    snap, DetentCentreZ(snap, fit), snap.Detent + snap.EngageClearance);

                result.Add(Place(Wedge(profile, length + 2), length + 2,
                                 side, RebateFace(side, fit), start - 1));
            }

            return result;
        }

        /// <summary>
        /// A shallow dish in the box's outer wall at the lid's bottom edge, so a thumb can
        /// push the lid off. Unioned into the box's negative.
        ///
        /// A cylinder lying along the wall with its axis outside the face, so only
        /// <see cref="LidGripDataModel.Depth"/> of it bites in. The dish is vertical where
        /// it meets the face and flattens toward the middle, so its downward-facing half
        /// stays inside the overhang limit - about 33 degrees at the defaults.
        /// </summary>
        public static List<OSCADObject> Grips(LidGripDataModel grip, LidFit fit)
        {
            var result = new List<OSCADObject>();

            // Radius that makes the dish stand Width/2 tall: for a circle cutting a
            // chord, halfHeight^2 = 2*R*Depth - Depth^2.
            var halfHeight = grip.Width / 4;
            var radius = (halfHeight * halfHeight + grip.Depth * grip.Depth) / (2 * grip.Depth);
            var axis = radius - grip.Depth;

            foreach (var side in grip.Sides)
            {
                var start = WallLength(side, fit) / 2 - grip.Width / 2;

                // Axis pushed back outside the face; the near side of the cylinder is
                // then exactly Depth proud of it.
                var dish = new Cylinder(radius * 2, grip.Width) { Resolution = 64 }
                    .Translate(-axis, 0, 0);

                result.Add(Place(dish, grip.Width, side, OuterFace(side, fit), start));
            }

            return result;
        }

        // =========================================================== validation

        public static void Validate(SnapFitDataModel snap, LidFit fit, string? owner = null)
        {
            var who = string.IsNullOrWhiteSpace(owner) ? "snap lid" : $"snap lid on '{owner}'";
            var slot = EffectiveSlotWidth(snap);

            if (snap.Sides.Count == 0)
            {
                throw new InvalidOperationException($"{who}: no sides given, so there is nothing to hold it shut.");
            }

            if (snap.Detent <= 0)
            {
                throw new InvalidOperationException($"{who}: detent must be positive, or nothing clicks.");
            }

            foreach (var (name, angle) in new[]
                     { ("leadInAngle", snap.LeadInAngle), ("retentionAngle", snap.RetentionAngle) })
            {
                if (angle is <= 0 or > 45)
                {
                    throw new InvalidOperationException(
                        $"{who}: {name} is {angle:0.##} degrees. It must be above 0 and at most 45 - " +
                        "one of these faces overhangs on the lid and the other on the box, so past 45 " +
                        "one of the two parts needs support material.");
                }
            }

            var needed = snap.ArmHeight + 2 * slot;
            if (fit.SkirtDepth < needed)
            {
                throw new InvalidOperationException(
                    $"{who}: the skirt is {fit.SkirtDepth:0.##} mm deep but the arm needs " +
                    $"{needed:0.##} (armHeight {snap.ArmHeight:0.##} plus a {slot:0.##} slot above and below). " +
                    "Raise lidBoxSize or lower armHeight.");
            }

            var detentHeight = Rise(snap.Detent, snap.LeadInAngle) + TipFlat + Rise(snap.Detent, snap.RetentionAngle);
            if (detentHeight > snap.ArmHeight)
            {
                throw new InvalidOperationException(
                    $"{who}: the detent stands {detentHeight:0.##} mm tall on a {snap.ArmHeight:0.##} mm arm, " +
                    "so it would run off the ends of the tongue. Raise armHeight, raise the angles, " +
                    "or lower detent.");
            }

            foreach (var side in snap.Sides)
            {
                var wall = WallLength(side, fit);
                if (snap.ArmLength + 2 * slot >= wall)
                {
                    throw new InvalidOperationException(
                        $"{who}: a {snap.ArmLength:0.##} mm arm does not fit the {wall:0.##} mm " +
                        $"{side} wall. Lower armLength.");
                }
            }
        }

        // ============================================================== layout

        private static double ArmBottom(SnapFitDataModel snap, LidFit fit) =>
            (fit.SkirtDepth - snap.ArmHeight) / 2;

        private static double DetentCentreZ(SnapFitDataModel snap, LidFit fit) =>
            ArmBottom(snap, fit) + snap.ArmHeight / 2;

        /// <summary>
        /// The bump sits on the free half of the arm, held a millimetre clear of the end
        /// slot so it cannot bridge it and weld the tongue back onto the skirt.
        /// </summary>
        public static (double Start, double Length) DetentSpan(SnapFitDataModel snap, double wall)
        {
            var armStart = wall / 2 - snap.ArmLength / 2;
            return (armStart + snap.ArmLength / 2, snap.ArmLength / 2 - 1);
        }

        public static double WallLength(SideType side, LidFit fit) =>
            side is SideType.LEFT or SideType.RIGHT ? fit.BoxSize.Y : fit.BoxSize.X;

        /// <summary>Where the lid's skirt presents its inner face, per side.</summary>
        public static double SkirtFace(SideType side, LidFit fit) => side switch
        {
            SideType.LEFT => fit.SkirtThickness,
            SideType.RIGHT => fit.BoxSize.X - fit.SkirtThickness,
            SideType.BOTTOM => fit.SkirtThickness,
            SideType.TOP => fit.BoxSize.Y - fit.SkirtThickness,
            _ => throw new ArgumentOutOfRangeException(nameof(side))
        };

        /// <summary>Where the box's rebated core presents its outer face, per side.</summary>
        public static double RebateFace(SideType side, LidFit fit) => side switch
        {
            SideType.LEFT => fit.Rebate,
            SideType.RIGHT => fit.BoxSize.X - fit.Rebate,
            SideType.BOTTOM => fit.Rebate,
            SideType.TOP => fit.BoxSize.Y - fit.Rebate,
            _ => throw new ArgumentOutOfRangeException(nameof(side))
        };

        /// <summary>The box's original outer face, below the rebate.</summary>
        private static double OuterFace(SideType side, LidFit fit) => side switch
        {
            SideType.LEFT => 0,
            SideType.RIGHT => fit.BoxSize.X,
            SideType.BOTTOM => 0,
            SideType.TOP => fit.BoxSize.Y,
            _ => throw new ArgumentOutOfRangeException(nameof(side))
        };

        // =========================================================== primitives

        /// <summary>
        /// The detent solid, in the canonical frame. Built as the hull of the slab that
        /// sits against the wall and the narrow bar at the tip - which gives exactly the
        /// four-point profile, tip flat included, without needing an extrude.
        /// </summary>
        private static OSCADObject Wedge(Vector2[] profile, double length)
        {
            var depth = profile[1].X;
            var back = new Cube(Eps, profile[3].Y - profile[0].Y, length)
                .Translate(0, profile[0].Y, 0);
            var tip = new Cube(Eps, profile[2].Y - profile[1].Y, length)
                .Translate(depth - Eps, profile[1].Y, 0);

            return back.Hull(tip);
        }

        // ========================================================== orientation

        /// <summary>
        /// Lands a solid built in the canonical frame onto a named wall.
        ///
        /// Canonical means: +X runs INWARD from the face, +Y is vertical, +Z runs along
        /// the wall. Every feature is authored once in that frame and each side supplies
        /// the rotation that carries it into place. Same approach as LabelHelper, and for
        /// the same reason - a sign error here produces geometry that looks entirely
        /// plausible and is mirrored.
        ///
        /// The length is passed rather than read off the object because a hull's bounds
        /// are not something this fork reports reliably.
        /// </summary>
        private static OSCADObject Place(
            OSCADObject solid, double length, SideType side, double face, double alongStart) =>
            side switch
            {
                // inward +X; the along axis lands on -Y, so shift forward by its length
                SideType.LEFT => solid.Rotate(90, 0, 0)
                    .Translate(face, alongStart + length, 0),

                // inward -X; along lands on +Y
                SideType.RIGHT => solid.Rotate(90, 0, 180)
                    .Translate(face, alongStart, 0),

                // inward +Y; along lands on +X
                SideType.BOTTOM => solid.Rotate(90, 0, 90)
                    .Translate(alongStart, face, 0),

                // inward -Y; along lands on -X
                SideType.TOP => solid.Rotate(90, 0, -90)
                    .Translate(alongStart + length, face, 0),

                _ => throw new ArgumentOutOfRangeException(nameof(side))
            };
    }
}
