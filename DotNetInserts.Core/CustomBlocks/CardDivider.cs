using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    public class CardDivider : OSCADObject
    {
        public CardDividerDataModel Model { get; }

        public CardDivider(CardDividerDataModel model)
        {
            Model = model;
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), Model.Size);
        }

        public override OSCADObject Clone()
        {
            return new CardDivider(Model);
        }

        public override Vector3 Position()
        {
            return Model.Size / 2;
        }

        public CardDivider WithTolerance()
        {
            var toleranceVector = new Vector3(Constants.Tolerance + 0.1, Constants.Tolerance, Constants.Tolerance);
            var size = Model.Size - toleranceVector;
            var bottomSize = Model.BottomCutoutSize - toleranceVector * 2;
            return new CardDivider(new CardDividerDataModel
            {
                Size = size,
                WallSize = Model.WallSize,
                BottomCutoutSize = new Vector3(size.X, bottomSize.Y, bottomSize.Z),
            });
        }

        public override string ToString()
        {
            /*var halfSize = Size.Y / 2;
            var cube = new Cube(Size)
                .Difference(
                    new Cube(Size.X, WallSize, Size.Z / 1.2),
                    new Cube(Size.X, WallSize, Size.Z / 1.2).Translate(0, Size.Y - WallSize, 0),
                    new Cube(Size.X, Size.Y / 1.5, Size.Z / 3).Translate(0, halfSize - Size.Y / 3, Size.Z - Size.Z / 3)
                    )
                .Union(
                    new Cube(BottomCutoutSize).Translate(0, (halfSize - BottomCutoutSize.Y) / 2, -BottomCutoutSize.Z),
                    new Cube(BottomCutoutSize).Translate(0, halfSize + (halfSize - BottomCutoutSize.Y) / 2, -BottomCutoutSize.Z));
            return cube.ToString();*/

            var halfSize = Model.Size.Y / 2;
            var middleSizeFromSide = 10;
            var differences = new List<OSCADObject>
            {
                //new Cube(Model.Size.X, Model.WallSize, Model.Size.Z / 1.2),
                //new Cube(Model.Size.X, Model.WallSize, Model.Size.Z / 1.2).Translate(0, Model.Size.Y - Model.WallSize, 0),
                new Cube(Model.Size.X / 2, Model.Size.Y - middleSizeFromSide, Model.Size.Z - middleSizeFromSide, true)
                    .Translate(Model.Size.X, Model.Size.Y / 2, Model.Size.Z / 2)
                    .Difference(new Cylinder(25, 5, center: true) { Resolution = 50}
                        .Rotate(0, 90, 0)
                        .Translate(Model.Size.X, Model.WallSize + 5, Model.Size.Z - 5)
                    ),
                new ScoopCutout(new Vector3(2, 20, 40), 5).Render(40)
                    .Translate(1, Model.Size.Y / 2 + 10, Model.Size.Z - 30),
            };

            if (!string.IsNullOrWhiteSpace(Model.ImagePath))
            {
                differences.Add(new ImageComponent(Model.ImagePath, .5)
                    .Resize(new Vector3(10, 10, 1))
                    .Rotate(90, 0, 90)
                    .Translate(Model.Size.X - .5, Model.WallSize + 2.5, Model.Size.Z - 5 - 2.5)
                );
            }

            var cube = new Cube(Model.Size)
                .Difference([.. differences])
                .Union(
                    new Cube(Model.BottomCutoutSize).Translate(0, (halfSize - Model.BottomCutoutSize.Y) / 2, -Model.BottomCutoutSize.Z),
                    new Cube(Model.BottomCutoutSize).Translate(0, halfSize + (halfSize - Model.BottomCutoutSize.Y) / 2, -Model.BottomCutoutSize.Z)
                );
            return cube.ToString();
        }
    }
}
