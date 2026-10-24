using DotNetInserts.Core.Helpers;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    public class InsertBox : OSCADObject, IPartWithExtraParts
    {
        public Vector3 Size { get; }
        public double? RoundRadius { get; }

        public Dictionary<IComponent, ComponentLocation> Components { get; }

        public InsertBoxLidDataModel? Lid { get; set; }

        /// <summary>Text engraved into the part's outer faces.</summary>
        public List<LabelDataModel> Labels { get; } = [];

        public InsertBox(Vector3 size, double? roundRadius = null)
        {
            Size = size;
            RoundRadius = roundRadius;

            Components = new Dictionary<IComponent, ComponentLocation>();
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), Size);
        }

        public override OSCADObject Clone()
        {
            return new InsertBox(Size, RoundRadius);
        }

        public override Vector3 Position()
        {
            return Size / 2;
        }

        public InsertBoxLid GetLid()
        {
            if (Lid is null) throw new ArgumentException("No lid has been configured");
            return new InsertBoxLid(this, Lid);
        }

        public override string ToString()
        {
            var differences = new List<OSCADObject>();

            if (Lid != null)
            {
                var lid = new InsertBoxLid(this, Lid);
                differences.Add(lid.Translate(0, 0, Size.Z - lid.Bounds().ZMax - (Lid.SpaceUnderLid ?? 0)));
            }

            foreach (var keyValue in Components)
            {
                if (!(keyValue.Key is OSCADObject component)) continue;

                var loc = LocationOf(component, keyValue.Value);

                // Tell the component how much wall surrounds it, so its cutouts can be
                // measured inward from the outer surface. Assigning the same value every
                // render keeps this idempotent.
                if (keyValue.Key is BoxComponent or CardTrayComponent)
                {
                    var bounds = component.Bounds();
                    var walls = new Models.WallThickness(
                        Top: Math.Max(0, Size.Y - (loc.Y + bounds.YMax)),
                        Right: Math.Max(0, Size.X - (loc.X + bounds.XMax)),
                        Bottom: Math.Max(0, loc.Y),
                        Left: Math.Max(0, loc.X));

                    // A card tray wraps its own BoxComponent, built fresh on every render,
                    // so the value has to land on the tray's model to survive the trip.
                    if (keyValue.Key is BoxComponent boxComponent) boxComponent.Model.Walls = walls;
                    else ((CardTrayComponent)keyValue.Key).Model.Walls = walls;
                }

                differences.Add(Place(component, component.Bounds(), keyValue.Value, loc));
            }

            // A box part has no single well floor, so a label with no side falls back to
            // engraving the top face. Naming a side is the useful case here.
            foreach (var label in Labels)
            {
                differences.Add(LabelHelper.Render(label, Size, Size.Z));
            }

            var cube = GetMainObject();

            if (differences.Count > 0)
            {
                cube = cube.Difference(differences.ToArray());
            }

            return cube.ToString();
        }

        private OSCADObject GetMainObject() => RoundedPrism.Build(Size, RoundRadius);

        /// <summary>
        /// Where a component's local origin lands in the part: centred in X/Y, hung from
        /// the top rim in Z, then nudged by the component's own location. The rim it hangs
        /// from is the lid's underside when there is one, since AddLid makes the box
        /// TopSize smaller to place the lid.
        ///
        /// Artwork is the exception - see <see cref="IVerticallyAnchored"/> - because it is
        /// cut into a face rather than opened through one.
        /// </summary>
        private Vector3 LocationOf(OSCADObject component, ComponentLocation placement)
        {
            var bounds = component.Bounds();
            var anchoredToBottom = component is IVerticallyAnchored { Anchor: Enums.VerticalAnchor.BOTTOM };

            var movement = new Vector3(
                (Size.X - bounds.XMax) / 2,
                (Size.Y - bounds.YMax) / 2,
                anchoredToBottom ? 0 : Size.Z - (Lid != null ? Lid.TopSize : 0) - bounds.ZMax);

            return movement + placement.Location;
        }

        /// <summary>
        /// Applies a component's rotation, scale and position. Split out of
        /// <see cref="ToString"/> so that an inlay exported from
        /// <see cref="GetExtraParts"/> is placed by the same code that placed the recess
        /// it fills - two copies of this arithmetic is two things to keep in step.
        ///
        /// <paramref name="bounds"/> is passed rather than read off
        /// <paramref name="geometry"/>: the rotation pivots on the component's own bounds,
        /// and an inlay's geometry is a transform chain whose reported bounds are not
        /// something to rely on (see docs/testing.md on composite bounds).
        /// </summary>
        private static OSCADObject Place(OSCADObject geometry, Bounds bounds, ComponentLocation placement, Vector3 location)
        {
            if (placement.Rotation != null)
            {
                geometry = geometry
                    .Translate(-bounds.XMax / 2, -bounds.YMax / 2, 0)
                    .Rotate(placement.Rotation)
                    .Translate(bounds.XMax / 2, bounds.YMax / 2, 0);
            }
            if (placement.Scale != null)
            {
                geometry = geometry.Scale(placement.Scale);
            }

            return geometry.Translate(location);
        }

        public OSCADObject[] GetExtraParts()
        {
            var items = new List<OSCADObject>();

            if (Lid is not null)
            {
                var lid = GetLid();
                items.Add(lid.WithTolerance());
                if (!string.IsNullOrWhiteSpace(lid.Model.ImagePath))
                {
                    items.Add(lid.GetImage());
                }
            }

            // A card tray only carves the slots its dividers sit in - the dividers are
            // separate bodies. Without this the tray prints with nothing to put in it,
            // which is what happened to every card tray built from JSON. One body per
            // tray: the dividers are interchangeable, so print as many copies as slots.
            foreach (var tray in Components.Keys.OfType<CardTrayComponent>())
            {
                items.Add(tray.GetDivider().WithTolerance());
            }

            // The other half of an engraved SVG: the same solid, placed exactly where the
            // recess is, so the two STLs can be dropped into the slicer as one object and
            // given two filaments. Exported in the part's own frame - see IAlignedWithPart.
            foreach (var keyValue in Components)
            {
                if (keyValue.Key is not SvgComponent svg || !svg.Model.Export) continue;

                items.Add(new ColourInlay(
                    Place(svg.Solid(), svg.Bounds(), keyValue.Value, LocationOf(svg, keyValue.Value))));
            }

            return items.ToArray();
        }
    }
}
