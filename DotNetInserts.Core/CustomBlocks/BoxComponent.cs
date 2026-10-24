using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Helpers;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    public class BoxComponent : OSCADObject, IComponent
    {
        public BoxComponentDataModel Model { get; }

        public BoxComponent(BoxComponentDataModel model)
        {
            Model = model;
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), Model.Size);
        }

        public override OSCADObject Clone()
        {
            return new BoxComponent(Model);
        }

        public override Vector3 Position()
        {
            return Model.Size / 2;
        }

        public override string ToString()
        {
            var cube = RenderBody();

            foreach (var cutout in Model.Cutouts)
            {
                cube = cube.Union(RenderCutout(cutout));
            }

            return cube.ToString();
        }

        private OSCADObject RenderBody()
        {
            if (!Model.RoundBottom)
            {
                return new Cube(Model.Size);
            }

            double bottomSize = Model.Size.Z / 4;
            return new Cube(Model.Size.X, Model.Size.Y, Model.Size.Z - bottomSize)
                .Translate(0, 0, bottomSize)
                .Union(new Cylinder(2) { Resolution = 50 }
                    .Rotate(-90, 0, 0)
                    .Scale(Model.Size.X / 2, Model.Size.Y, bottomSize)
                    .Translate(Model.Size.X / 2, 0, bottomSize));
        }

        private OSCADObject RenderCutout(Cutout cutout)
        {
            var declaredHeight = cutout.Shape.DeclaredHeight;

            // Notch: exactly as tall as declared, hanging from the top rim.
            // Through: the full component height plus the declared height, so it clears
            // the floor and overshoots it rather than leaving a skin behind.
            var height = cutout.Mode == CutoutMode.Through
                ? declaredHeight + Model.Size.Z
                : declaredHeight;

            // For top and left the wall sits at the profile's far face; for bottom and
            // right it sits at the near one, so an asymmetric profile has to be flipped.
            var mirrorDepth = cutout.Side is SideType.BOTTOM or SideType.RIGHT;
            var shape = cutout.Shape.Render(height, mirrorDepth);

            // Left and right walls run along Y, so the shape is turned to match. Its X
            // therefore always measures along the wall it is cut into, whichever side.
            var turned = cutout.Side is SideType.RIGHT or SideType.LEFT;

            // Placement is measured from the shape WITHOUT its offset. If the offset were
            // already baked in, anchoring the result to the wall would simply cancel it.
            var reference = turned ? shape.Rotate(0, 0, 90) : shape;

            var offset = PositionalHelper.CalculatePositionOffset(
                Model.Size, reference.Bounds(), cutout.Side, Model.Walls.For(cutout.Side));

            offset.Z = cutout.Mode == CutoutMode.Through
                ? -declaredHeight
                : Model.Size.Z - declaredHeight;

            // The user's offset moves the shape in its own frame, so it is applied before
            // the quarter turn - a nudge along X follows the shape onto a side wall.
            var body = cutout.Shape.Offset is null ? shape : shape.Translate(cutout.Shape.Offset);
            if (turned)
            {
                body = body.Rotate(0, 0, 90);
            }

            return body.Translate(offset);
        }
    }
}
