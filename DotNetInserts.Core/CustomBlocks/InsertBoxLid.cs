using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Helpers;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    /// <summary>
    /// A shoe-box lid: a top plate with a skirt that drops over a rebate cut into the top
    /// of the box.
    ///
    /// There are two fit models here. The original one derives the skirt thickness and
    /// the clearance from a single constant, which makes them impossible to set
    /// independently - see <see cref="LegacyTolerance"/>. The explicit model
    /// (<see cref="InsertBoxLidDataModel.UsesExplicitFit"/>) takes both as real inputs,
    /// and is what a snap fit needs. Files that do not ask for the new one keep the old
    /// geometry exactly.
    /// </summary>
    public class InsertBoxLid : OSCADObject
    {
        /// <summary>
        /// The original fit constant. It is not a clearance in any direct sense: it is
        /// subtracted from the box size to size the lid cavity AND used to inset that
        /// cavity, which nets out to 0.75 mm of air per side no matter what wallSize is,
        /// and a printed skirt of (wallSize - 1.5)/2 - unprintable below about 3 mm of
        /// wall. Preserved only so existing files render unchanged.
        /// </summary>
        private const double LegacyTolerance = 1.5;

        private readonly InsertBox _box;
        public InsertBoxLidDataModel Model { get; }

        public InsertBoxLid(InsertBox box, InsertBoxLidDataModel model)
        {
            _box = box;

            Model = model;
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), new Vector3(_box.Size.X, _box.Size.Y, Model.LidBoxSize ?? _box.Size.Z / 2));
        }

        public override OSCADObject Clone()
        {
            return new InsertBoxLid(_box, Model);
        }

        public override Vector3 Position()
        {
            return new Vector3(_box.Size.X, _box.Size.Y, _box.Size.Z / 2) / 2;
        }

        public OSCADObject GetImage()
        {
            return new ImageComponent(Model.ImagePath, 1)
                    .Translate(new Vector3(_box.Size.X / 2, _box.Size.Y / 2, _box.Size.Z / 2 - 1));
        }

        /// <summary>The resolved joint geometry. Only meaningful on the explicit path.</summary>
        public LidFit Fit()
        {
            var fit = Model.ResolveFit(_box.Size, _box.RoundRadius);

            if (Model.Snap is not null)
            {
                SnapFitHelper.Validate(Model.Snap, fit, Model.OwnerName);
                CheckCatchesClearTheContents(fit);
            }

            return fit;
        }

        /// <summary>
        /// The catch pockets cut into a wall that the rebate has already thinned. If a
        /// component sits close behind it, the pocket opens into the cavity - a hole
        /// straight through the side of the box, which nothing else here would notice
        /// because both solids are individually valid.
        /// </summary>
        private void CheckCatchesClearTheContents(LidFit fit)
        {
            var pocket = Model.Snap!.Detent + Model.Snap.EngageClearance;
            var who = string.IsNullOrWhiteSpace(Model.OwnerName) ? "snap lid" : $"snap lid on '{Model.OwnerName}'";

            foreach (var keyValue in _box.Components)
            {
                if (keyValue.Key is not OSCADObject component) continue;

                // Components are centred in X/Y, then nudged by their own location -
                // the same placement InsertBox.ToString() applies.
                var bounds = component.Bounds();
                var x = (_box.Size.X - bounds.XMax) / 2 + keyValue.Value.Location.X;
                var y = (_box.Size.Y - bounds.YMax) / 2 + keyValue.Value.Location.Y;

                foreach (var side in Model.Snap.Sides)
                {
                    var wall = side switch
                    {
                        SideType.LEFT => x - fit.Rebate,
                        SideType.RIGHT => _box.Size.X - (x + bounds.XMax) - fit.Rebate,
                        SideType.BOTTOM => y - fit.Rebate,
                        SideType.TOP => _box.Size.Y - (y + bounds.YMax) - fit.Rebate,
                        _ => double.MaxValue
                    };

                    if (wall < pocket + Model.MinWall)
                    {
                        throw new InvalidOperationException(
                            $"{who}: the {side} catch would cut {pocket:0.##} mm into a wall only " +
                            $"{wall:0.##} mm thick behind the rebate, breaking through into the " +
                            "cavity. Move the snap to another side, shrink the component, or lower detent.");
                    }
                }
            }
        }

        // ===================================================================== printed

        /// <summary>The lid as it gets printed.</summary>
        public OSCADObject WithTolerance()
        {
            EnsureLidBoxSize();
            return Model.UsesExplicitFit ? PrintedLid(Fit()) : LegacyPrintedLid();
        }

        private OSCADObject PrintedLid(LidFit fit)
        {
            var body = RoundedPrism.Build(
                new Vector3(fit.BoxSize.X, fit.BoxSize.Y, fit.LidHeight), fit.LidCornerDiameter);

            var differences = new List<OSCADObject>
            {
                // The cavity the box's rebated top sits in. Stopping it TopThickness short
                // of the lid's height is what leaves the solid top plate.
                RoundedPrism.Build(
                        new Vector3(fit.SkirtInnerX, fit.SkirtInnerY, fit.SkirtDepth),
                        fit.SkirtCornerDiameter)
                    .Translate(fit.SkirtThickness, fit.SkirtThickness, -0.01)
            };

            if (Model.Snap is not null)
            {
                differences.AddRange(SnapFitHelper.ArmSlots(Model.Snap, fit));
            }

            var lid = body.Difference([.. differences]);

            if (Model.Snap is not null)
            {
                lid = lid.Union([.. SnapFitHelper.Detents(Model.Snap, fit)]);
            }

            var honeycomb = BuildHoneycomb(fit.LidHeight);
            return honeycomb is null ? lid : lid.Difference(honeycomb);
        }

        private OSCADObject LegacyPrintedLid()
        {
            var innerSizeX = _box.Size.X - Model.WallSize + LegacyTolerance;
            var innerSizeY = _box.Size.Y - Model.WallSize + LegacyTolerance;
            var unions = new List<OSCADObject>();
            var differences = new List<OSCADObject>();

            if (!string.IsNullOrWhiteSpace(Model.ImagePath))
            {
                differences.Add(new ImageComponent(Model.ImagePath, 1)
                    .Translate(new Vector3(_box.Size.X / 2, _box.Size.Y / 2, Model.LidBoxSize!.Value - 1)));
            }
            differences.Add(new Cube(innerSizeX, innerSizeY, Model.LidBoxSize!.Value - Model.TopSize)
                    .Translate((Model.WallSize - LegacyTolerance) / 2, (Model.WallSize - LegacyTolerance) / 2, 0));

            var honeycomb = BuildHoneycomb(Model.LidBoxSize.Value);
            if (honeycomb is not null)
            {
                differences.Add(honeycomb);
            }

            OSCADObject cube = new Cube(_box.Size.X, _box.Size.Y, Model.LidBoxSize.Value);
            if (unions.Count > 0)
            {
                cube = cube.Union([.. unions]);
            }
            cube.Difference([.. differences]).ToFile("Test.scad");
            return cube.Difference([.. differences]);
        }

        // ==================================================================== negative

        /// <summary>
        /// The volume subtracted from the box: it carves the rebate the skirt drops over
        /// and clears the space the top plate occupies.
        /// </summary>
        public OSCADObject Render()
        {
            EnsureLidBoxSize();
            return Model.UsesExplicitFit ? RenderNegative(Fit()) : LegacyNegative();
        }

        private OSCADObject RenderNegative(LidFit fit)
        {
            var body = RoundedPrism.Build(
                new Vector3(fit.BoxSize.X, fit.BoxSize.Y, fit.LidHeight), fit.LidCornerDiameter);

            // What the box KEEPS: the rebated core, only as tall as the skirt is deep.
            // Above it the negative stays solid, which is what removes the box's top plate.
            var core = RoundedPrism.Build(
                    new Vector3(fit.RebateOuterX, fit.RebateOuterY, fit.SkirtDepth),
                    fit.RebateCornerDiameter)
                .Translate(fit.Rebate, fit.Rebate, -0.01);

            var negative = body.Difference(core);

            // The catch pockets are part of the negative, so they get cut into the box.
            if (Model.Snap is not null)
            {
                negative = negative.Union([.. SnapFitHelper.Catches(Model.Snap, fit)]);
            }

            if (Model.Grip is not null)
            {
                negative = negative.Union([.. SnapFitHelper.Grips(Model.Grip, fit)]);
            }

            return negative;
        }

        private OSCADObject LegacyNegative()
        {
            var unions = new List<OSCADObject>();

            OSCADObject cube = new Cube(_box.Size.X, _box.Size.Y, Model.LidBoxSize!.Value + (Model.SpaceUnderLid ?? 0));
            if (unions.Count > 0)
            {
                cube = cube.Union([.. unions]);
            }

            var differences = new List<OSCADObject>
            {
                new Cube(_box.Size.X - Model.WallSize, _box.Size.Y - Model.WallSize, Model.LidBoxSize.Value - Model.TopSize + (Model.SpaceUnderLid ?? 0))
                        .Translate(Model.WallSize / 2, Model.WallSize / 2, 0)
            };
            if (Model.SpaceUnderLid.HasValue)
            {
                differences.AddRange(
                    [new Cube(_box.Size.X - Model.WallSize - 60, _box.Size.Y, Model.SpaceUnderLid.Value)
                        .Translate(Model.WallSize / 2 + 30, 0, 0),
                    new Cube(_box.Size.X, _box.Size.Y - Model.WallSize - 60, Model.SpaceUnderLid.Value)
                        .Translate(0, Model.WallSize / 2 + 30, 0)]);
            }

            if (!string.IsNullOrWhiteSpace(Model.ImagePath))
            {
                differences.Add(new ImageComponent(Model.ImagePath, 1)
                    .Resize(new Vector3(80, 80, 1))
                    .Translate(new Vector3(_box.Size.X / 2 - 40, _box.Size.Y / 2, Model.LidBoxSize.Value - 1)));
            }

            return cube
                .Difference(
                    [.. differences]
                  );
        }

        // ===================================================================== shared

        /// <summary>
        /// Fills in the default lid height. Writes back into the shared model, as it
        /// always has - several call sites read Model.LidBoxSize afterwards and expect it
        /// to be populated.
        /// </summary>
        private void EnsureLidBoxSize()
        {
            Model.LidBoxSize ??= _box.Size.Z / 2;
        }

        /// <summary>
        /// The hexagonal cut-out grid through the top plate, or null when there is none.
        /// Punches through with 0.1 mm of overshoot at both faces so no face is coplanar.
        /// </summary>
        private OSCADObject? BuildHoneycomb(double lidHeight)
        {
            if (Model.PatternData is null) return null;

            var region = new Vector3(Model.PatternData.Size.X, Model.PatternData.Size.Y, Model.TopSize + .2);
            var radius = Model.PatternData.Radius;
            var distanceBetween = Model.PatternData.DistanceBetween;
            OSCADObject? firstObject = null;

            var yStep = Math.Sqrt(3) * radius;
            var xDifference = radius * 2 - Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(yStep / 2, 2));
            var amountOfY = region.Y / yStep;
            var fullY = (yStep + distanceBetween) * (Math.Ceiling(amountOfY) - 1) + yStep;
            var amountOfX = region.X / xDifference;
            var fullX = (xDifference + distanceBetween) * (Math.Ceiling(amountOfX) - 1) + radius * 2;

            var startY = yStep / 2 - (fullY - region.Y) / 2;
            var y = startY;
            var startX = radius - (fullX - region.X) / 2;
            double x = startX;

            var unionsList = new List<OSCADObject>();
            for (var i = 0; i < amountOfX; i++)
            {
                for (var j = 0; j < amountOfY; j++)
                {
                    var honeyComb = new Cylinder(radius * 2, region.Z) { Resolution = 6 }.Translate(x, y, 0);
                    if (firstObject is null)
                        firstObject = honeyComb;
                    else
                        unionsList.Add(honeyComb);

                    y += yStep + distanceBetween;
                }

                x += xDifference + distanceBetween;
                y = (i % 2 == 0 ? startY + yStep / 2 + (double)distanceBetween / 2 : startY);
            }

            return firstObject?.Union([.. unionsList])
                .Translate(
                    (_box.Size.X - region.X) / 2,
                    (_box.Size.Y - region.Y) / 2 - radius / 2,
                    lidHeight - Model.TopSize - .1);
        }

        public override string ToString()
        {
            return Render().ToString();
        }
    }
}
