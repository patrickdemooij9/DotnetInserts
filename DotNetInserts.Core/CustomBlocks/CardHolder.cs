using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Helpers;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Solids.Compound;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    public class CardHolder : OSCADObject, IComponent
    {
        public CardHolderModel Model { get; }

        public CardHolder(CardHolderModel model)
        {
            Model = model;
        }

        public override Vector3 Position()
        {
            return Model.Size / 2;
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), Model.Size);
        }

        public override OSCADObject Clone()
        {
            return new CardHolder(Model);
        }

        public override string ToString()
        {
            var box = new InsertBox(Model.Size, Model.RoundedCorners ? 7.5 : null);
            var boxComponentData = new BoxComponentDataModel(Model.InnerSize);
            if (Model.CutoutSide is SideType.BOTTOM or SideType.LEFT or SideType.RIGHT)
            {
                // A through-cut that opens the floor, plus a scoop notched into the same
                // wall so a finger can reach the cards.
                boxComponentData.Cutouts.Add(new Cutout(Model.CutoutSide, CutoutMode.Through,
                    new CutoutCube(new Vector3(Model.Size.X / 3 * 2, Model.Size.Y / 2, Model.Size.Z - Model.InnerSize.Z + .1))));
                boxComponentData.Cutouts.Add(new Cutout(Model.CutoutSide, CutoutMode.Notch,
                    new ScoopCutout(new Vector3(Model.Size.X / 3 * 2, 4, Model.Size.Z), 4)));
            }

            box.Components.Add(new BoxComponent(boxComponentData), new ComponentLocation(new Vector3()));

            /*var stackableSizeBottom = new Vector3(Model.InnerSize.X, Model.InnerSize.Y, .8);
            var stackable = new Cube(stackableSizeBottom + new Vector3(2, 2, 0))
                .Difference(new Cube(stackableSizeBottom)
                    .Translate(1, 1, 0))
                .Translate((Model.Size.X - Model.InnerSize.X) / 2 - 0.75, (Model.Size.Y - Model.InnerSize.Y) / 2 - 0.75, 0);

            var stackableSizeTop = new Vector3(Model.InnerSize.X, Model.InnerSize.Y, .6);
            var stackable2 = new Cube(stackableSizeTop + new Vector3(1.5, 1.5, 0))
                .Difference(new Cube(stackableSizeTop)
                    .Translate(.75, .75, 0),
                    new Cube(new Vector3(40, 2, 1.1))
                    .Translate((stackableSizeTop.X + 1.5 - 40) / 2, 0, 0))
                .Translate((Model.Size.X - Model.InnerSize.X) / 2 - .75, (Model.Size.Y - Model.InnerSize.Y) / 2 - .75, Model.Size.Z - .6);*/

            /*var stackableSizeBottom = new Vector3(Model.InnerSize.X, Model.InnerSize.Y, 2) - new Vector3(4, 4, 0);
            var stackableBottom = new Cube(stackableSizeBottom)
                .Translate((Model.Size.X - stackableSizeBottom.X) / 2, (Model.Size.Y - stackableSizeBottom.Y) / 2, -2);

            var paths = new Polygon([new Vector2(0.8, 0), new Vector2(0.8, 1), new Vector2(2, 2), new Vector2(0, 2), new Vector2()]);

            return stackableBottom
                .Union(
                    paths.RotateExtrude(90).Rotate(0, 0, 180).Translate(4, 4, -2),
                    paths.RotateExtrude(90).Rotate(0, 0, 90).Translate(4, 4 + Model.InnerSize.Y, -2),
                    paths.RotateExtrude(90).Rotate(0, 0, 180).Translate(2, 2, -2),
                    paths.RotateExtrude(90).Rotate(0, 0, 180).Translate(2, 2, -2),
                    paths.LinearExtrude(10).Rotate(90, 0, 0)
                )
                .ToString()!;*/

            //return box.Difference(stackable).Union(stackable2).ToString()!;

            var stackPointSize = new Vector3(10, .8, 3);
            var stackPoint = new Cube(stackPointSize);
            var removeStackPoint = new Cube(stackPointSize + new Vector3(Constants.Tolerance, Constants.Tolerance, Constants.Tolerance));

            OSCADObject result = box;
            if (Model.StackableTop)
            {
                result = result.Union(
                    stackPoint.Translate((box.Size.X - 10) / 2, 0, box.Size.Z),
                    stackPoint.Translate((box.Size.X - 10) / 2, box.Size.Y - 1.2, box.Size.Z)
                    );
            }
            if (Model.StackableBottom)
            {
                result = result.Difference(
                    removeStackPoint.Translate((box.Size.X - removeStackPoint.Size.X) / 2, 0, 0),
                    removeStackPoint.Translate((box.Size.X - removeStackPoint.Size.X) / 2, box.Size.Y - removeStackPoint.Size.Y, 0)
                );
            }
            // The floor of the well, which is where a label with no side goes.
            var floorZ = Model.Size.Z - Model.InnerSize.Z;

            foreach (var label in GetLabels())
            {
                result = result.Difference(LabelHelper.Render(label, Model.Size, floorZ));
            }
            return result.ToString()!;
        }

        /// <summary>
        /// The labels to cut, with the legacy <see cref="CardHolderModel.Text"/> folded in
        /// as a floor label. Folding it in here rather than in the mapper keeps it working
        /// for the projects that build a CardHolderModel directly.
        /// </summary>
        private IEnumerable<LabelDataModel> GetLabels()
        {
            if (!string.IsNullOrWhiteSpace(Model.Text))
            {
                yield return new LabelDataModel
                {
                    Text = Model.Text,
                    FontSize = Model.TextSize
                };
            }

            foreach (var label in Model.Labels)
            {
                yield return label;
            }
        }
    }
}
