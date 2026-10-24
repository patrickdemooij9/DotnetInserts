using System.Globalization;
using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Helpers;
using DotNetInserts.Core.Models;
using DotNetInserts.Core.Validation;
using DotNetInserts.Tests.Infrastructure;
using OSCADSharp.Spatial;
using Xunit;

namespace DotNetInserts.Tests
{
    /// <summary>
    /// The snap-fit lid, and the explicit fit model underneath it.
    ///
    /// These assert on computed millimetres rather than on emitted OpenSCAD, because the
    /// properties that matter here are not visible in the script: how much air is left in
    /// the joint, and whether a face overhangs. Neither would fail a snapshot, and the
    /// snapshot cannot fail anyway while Snapshot.UpdateMode is hardcoded true.
    /// </summary>
    public class SnapLidTests
    {
        private static readonly Vector3 BoxSize = new(80, 60, 40);

        private static InsertBoxLidDataModel Lid(
            double wallSize = 3, double? skirt = 1.6, double? clearance = 0.15,
            SnapFitDataModel? snap = null, LidGripDataModel? grip = null) =>
            new(wallSize, topSize: 2)
            {
                LidBoxSize = 10,
                SkirtThickness = skirt,
                Clearance = clearance,
                Snap = snap,
                Grip = grip
            };

        private static LidFit Fit(InsertBoxLidDataModel model) => model.ResolveFit(BoxSize, null);

        // ============================================================== the fit

        /// <summary>
        /// The property the old model could not hold: clearance is what you asked for, and
        /// it does not drift when the wall changes. Previously both the skirt and the
        /// clearance came out of one constant, so this varied with wallSize and the skirt
        /// went unprintable below about 3 mm.
        /// </summary>
        [Theory]
        [InlineData(1.6)]
        [InlineData(3)]
        [InlineData(6)]
        public void Clearance_is_independent_of_wall_thickness(double wallSize)
        {
            var fit = Fit(Lid(wallSize, skirt: 1.6, clearance: 0.15));

            Assert.Equal(0.15, fit.ClearancePerSide, 6);
            Assert.Equal(1.6, fit.SkirtThickness, 6);
        }

        [Fact]
        public void Rebate_is_the_skirt_plus_its_clearance()
        {
            var fit = Fit(Lid(skirt: 2, clearance: 0.2));

            Assert.Equal(2.2, fit.Rebate, 6);
            Assert.Equal(BoxSize.X - 2 * 2.2, fit.RebateOuterX, 6);
            Assert.Equal(BoxSize.X - 2 * 2, fit.SkirtInnerX, 6);
        }

        [Fact]
        public void A_skirt_thinner_than_the_minimum_is_rejected()
        {
            var model = Lid(skirt: 0.3);
            model.OwnerName = "Tray";

            var ex = Assert.Throws<InvalidOperationException>(() => Fit(model));
            Assert.Contains("Tray", ex.Message);
            Assert.Contains("skirt", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void A_lid_with_no_new_keys_stays_on_the_original_path()
        {
            var plain = new InsertBoxLidDataModel(3, 2) { LidBoxSize = 10 };
            Assert.False(plain.UsesExplicitFit);

            Assert.True(Lid().UsesExplicitFit);
            Assert.True(new InsertBoxLidDataModel(3, 2) { Snap = new SnapFitDataModel() }.UsesExplicitFit);
        }

        [Fact]
        public void Snap_and_spaceUnderLid_are_refused_rather_than_guessed_at()
        {
            var model = Lid(snap: new SnapFitDataModel());
            model.SpaceUnderLid = 1;

            var ex = Assert.Throws<InvalidOperationException>(() => Fit(model));
            Assert.Contains("spaceUnderLid", ex.Message);
        }

        // =========================================================== the detent

        /// <summary>
        /// The whole design hangs on this. The retention face overhangs on the lid, which
        /// prints inverted; the lead-in face overhangs on the box, which prints upright.
        /// Past 45 degrees from vertical one of the two parts needs support material, and
        /// nothing downstream would notice.
        /// </summary>
        [Fact]
        public void Every_detent_face_is_inside_the_overhang_limit()
        {
            var snap = new SnapFitDataModel();
            var profile = SnapFitHelper.DetentProfile(snap, centreZ: 5, depth: snap.Detent);

            for (var i = 0; i < profile.Length - 1; i++)
            {
                var run = Math.Abs(profile[i + 1].X - profile[i].X);
                var rise = Math.Abs(profile[i + 1].Y - profile[i].Y);
                if (run == 0) continue; // the tip flat

                var fromVertical = Math.Atan2(run, rise) * 180 / Math.PI;
                Assert.True(fromVertical <= 45.001,
                    $"face {i} sits {fromVertical:0.#} degrees off vertical, past the 45 limit");
            }
        }

        [Fact]
        public void The_detent_leads_in_shallower_than_it_holds()
        {
            var snap = new SnapFitDataModel();
            var profile = SnapFitHelper.DetentProfile(snap, centreZ: 5, depth: snap.Detent);

            // Lower face is the lead-in: it climbs further for the same depth, so it is
            // the gentler ramp to push the lid on over.
            var lead = profile[1].Y - profile[0].Y;
            var retain = profile[3].Y - profile[2].Y;

            Assert.True(lead > retain, "the lead-in should be the shallower of the two faces");
            Assert.Equal(snap.Detent, profile[1].X, 6);
        }

        [Fact]
        public void The_detent_has_a_flat_tip_rather_than_a_knife_edge()
        {
            var profile = SnapFitHelper.DetentProfile(new SnapFitDataModel(), centreZ: 5, depth: 0.5);

            Assert.Equal(profile[1].X, profile[2].X, 6);
            Assert.Equal(SnapFitHelper.TipFlat, profile[2].Y - profile[1].Y, 6);
        }

        [Fact]
        public void The_catch_is_cut_deeper_than_the_bump_stands_proud()
        {
            var snap = new SnapFitDataModel();
            var bump = SnapFitHelper.DetentProfile(snap, 5, snap.Detent);
            var pocket = SnapFitHelper.DetentProfile(snap, 5, snap.Detent + snap.EngageClearance);

            Assert.Equal(snap.EngageClearance, pocket[1].X - bump[1].X, 6);
        }

        [Fact]
        public void Zero_slot_width_means_a_fused_slot_not_no_slot()
        {
            // A literal zero would emit nothing at all and the lid would come out solid.
            var fused = SnapFitHelper.EffectiveSlotWidth(new SnapFitDataModel { SlotWidth = 0 });

            Assert.Equal(SnapFitHelper.FusedSlotWidth, fused, 6);
            Assert.True(fused > 0);
            Assert.True(fused < SnapFitHelper.FusedSlotCeiling,
                "a breakaway slot has to be narrower than one extrusion width or it prints as a gap");
        }

        // ============================================================ placement

        [Fact]
        public void The_bump_stays_clear_of_the_slot_that_frees_the_arm()
        {
            var snap = new SnapFitDataModel();
            var wall = 60.0;
            var (start, length) = SnapFitHelper.DetentSpan(snap, wall);
            var armEnd = wall / 2 + snap.ArmLength / 2;

            Assert.True(start >= wall / 2 - snap.ArmLength / 2, "bump starts inside the arm");
            Assert.True(start + length < armEnd,
                "bump must not reach the end slot, or it would weld the tongue back on");
        }

        [Fact]
        public void The_arm_must_fit_the_skirt_it_is_cut_into()
        {
            // 5 mm arm plus a 0.6 slot above and below needs 6.2 of skirt; this lid has 4.
            var model = Lid(snap: new SnapFitDataModel { ArmHeight = 5, SlotWidth = 0.6 });
            model.LidBoxSize = 6;
            model.OwnerName = "Tray";

            var box = new InsertBox(BoxSize) { Lid = model };
            var ex = Assert.Throws<InvalidOperationException>(() => box.GetLid().Fit());

            Assert.Contains("skirt", ex.Message);
            Assert.Contains("armHeight", ex.Message);
        }

        [Fact]
        public void An_arm_longer_than_its_wall_is_rejected()
        {
            var model = Lid(snap: new SnapFitDataModel { ArmLength = 200 });
            var box = new InsertBox(BoxSize) { Lid = model };

            Assert.Throws<InvalidOperationException>(() => box.GetLid().Fit());
        }

        [Theory]
        [InlineData(60)]
        [InlineData(90)]
        public void Angles_past_the_overhang_limit_are_rejected(double angle)
        {
            var model = Lid(snap: new SnapFitDataModel { RetentionAngle = angle });
            var box = new InsertBox(BoxSize) { Lid = model };

            var ex = Assert.Throws<InvalidOperationException>(() => box.GetLid().Fit());
            Assert.Contains("support material", ex.Message);
        }

        /// <summary>
        /// The catch cuts into a wall the rebate has already thinned. If a component sits
        /// right behind it the pocket breaks through into the cavity - a hole in the side
        /// of the box that no bounds or overlap check would see, because both solids stay
        /// individually valid.
        /// </summary>
        [Fact]
        public void A_catch_that_would_break_into_the_cavity_is_rejected()
        {
            var model = Lid(snap: new SnapFitDataModel());
            model.OwnerName = "Tray";

            var box = new InsertBox(BoxSize) { Lid = model };
            // Well leaves only 1 mm of wall; the rebate alone eats 1.75 of it.
            box.Components.Add(
                new BoxComponent(new BoxComponentDataModel(new Vector3(78, 58, 30))),
                new ComponentLocation(new Vector3()));

            var ex = Assert.Throws<InvalidOperationException>(() => box.GetLid().Fit());
            Assert.Contains("cavity", ex.Message);
        }

        // ============================================================== output

        [Fact]
        public void A_snap_lid_renders_both_halves_of_the_joint()
        {
            var insert = InsertAnalyzer.LoadFile(Fixtures.PathOf("Features/SnapCoupon"));

            var box = InsertAnalyzer.BuildCombinedScad(insert);
            var extras = InsertAnalyzer.BuildExtraPartsScad(insert);

            Assert.Contains("hull", extras);           // the detents on the lid
            Assert.NotEqual("$fn=48;", extras.Trim()); // the lid reaches the snapshot at all
            Assert.Contains("difference", box);
        }

        /// <summary>
        /// This fork writes linear_extrude's height with raw interpolation and no
        /// invariant culture, so on a comma-decimal machine `height=14.6` becomes
        /// `height=14,6` and OpenSCAD reads it as two arguments. The snap geometry is
        /// built from Cube and Cylinder to sidestep that; this pins it.
        /// </summary>
        [Theory]
        [InlineData("nl-NL")]
        [InlineData("de-DE")]
        [InlineData("en-US")]
        public void Generated_scad_does_not_depend_on_the_machines_number_format(string culture)
        {
            var original = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                var insert = InsertAnalyzer.LoadFile(Fixtures.PathOf("Features/SnapCoupon"));
                var extras = InsertAnalyzer.BuildExtraPartsScad(insert);

                Assert.DoesNotContain("linear_extrude", extras);
                Assert.DoesNotMatch(@"=\s*-?\d+,\d", extras);
            }
            finally
            {
                CultureInfo.CurrentCulture = original;
            }
        }
    }
}
