using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    /// <summary>
    /// An SVG engraved into a face of the part, and printed again on its own so the
    /// recess can be filled in a second colour.
    ///
    /// It is a component rather than a part-level property like <see cref="LabelDataModel"/>
    /// because, unlike OpenSCAD's text primitive, an import can be given a footprint - so
    /// it reports honest bounds and the existing placement maths works on it unchanged.
    /// The one thing it does differently is where Z is measured from: see
    /// <see cref="IVerticallyAnchored"/>.
    /// </summary>
    public class SvgComponent : OSCADObject, IComponent, IVerticallyAnchored
    {
        public SvgComponentDataModel Model { get; }

        public VerticalAnchor Anchor => Model.Anchor;

        public SvgComponent(SvgComponentDataModel model)
        {
            Model = model;
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), Model.Size);
        }

        public override OSCADObject Clone()
        {
            return new SvgComponent(Model);
        }

        public override Vector3 Position()
        {
            return new Vector3();
        }

        /// <summary>
        /// The artwork as a solid occupying [0, Size], which is both the volume cut out of
        /// the part and - byte for byte the same geometry - the body printed to fill it.
        /// No clearance between the two on purpose: these are meant to be loaded into the
        /// slicer together and printed as one object, where a gap is a defect, not a fit.
        /// </summary>
        public OSCADObject Solid()
        {
            // Size is handed to ImageComponent as well so its Bounds() is sane; the import
            // itself is centred on the origin, hence the resize-then-translate order.
            OSCADObject solid = new ImageComponent(Model.Path, Model.Size.Z) { Size = Model.Size }
                .Resize(Model.Size);

            if (Model.Mirrored)
                solid = solid.Mirror(1, 0, 0);

            return solid.Translate(Model.Size.X / 2, Model.Size.Y / 2, 0);
        }

        public override string ToString()
        {
            return Solid().ToString()!;
        }
    }
}
