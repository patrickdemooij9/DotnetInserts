using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.Enums;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Solids;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.CustomBlocks
{
    public class CardTrayComponent : OSCADObject, IComponent
    {
        public CardTrayDataModel Model { get; }

        public CardTrayComponent(CardTrayDataModel model)
        {
            Model = model;
        }

        public override Bounds Bounds()
        {
            return new Bounds(new Vector3(), Model.Size);
        }

        public override OSCADObject Clone()
        {
            return new CardTrayComponent(Model);
        }

        public override Vector3 Position()
        {
            return Model.Size / 2;
        }

        public CardDivider GetDivider()
        {
            // Z comes from the divider, not the tray, so the body matches the slot the
            // loop below carves - those two used to disagree whenever a tray was given a
            // divider shorter than itself, and the printed divider would not go in.
            return new CardDivider(new CardDividerDataModel
            {
                Size = new Vector3(Model.Divider.Model.Size.X, Model.Size.Y + Model.Divider.Model.WallSize * 2, Model.Divider.Model.Size.Z),
                WallSize = Model.Divider.Model.WallSize,
                BottomCutoutSize = Model.Divider.Model.BottomCutoutSize
            });
        }

        public override string ToString()
        {
            // Walls has to be carried across or the end scoops below are anchored to the
            // cavity edge instead of the part's outer surface, and quietly cut nothing.
            var boxComponentModel = new BoxComponentDataModel(Model.Size) { Walls = Model.Walls };
            boxComponentModel.Cutouts.Add(new Cutout(SideType.RIGHT, CutoutMode.Notch, new ScoopCutout(new Vector3(40, 20, 40), 8)));
            boxComponentModel.Cutouts.Add(new Cutout(SideType.LEFT, CutoutMode.Notch, new ScoopCutout(new Vector3(40, 20, 40), 8)));

            var boxComponent = new BoxComponent(boxComponentModel);

            var trayObjects = new List<OSCADObject>();
            var availableSpace = Model.Size.X - Model.Divider.Model.Size.X * Model.AmountOfDividers;
            var spaceBetween = availableSpace / (Model.AmountOfDividers + 1);
            for (var i = 0; i < Model.AmountOfDividers; i++)
            {
                OSCADObject divider = new CardDivider(new CardDividerDataModel
                {
                    Size = new Vector3(Model.Divider.Model.Size.X, Model.Size.Y + Model.Divider.Model.WallSize * 2, Model.Divider.Model.Size.Z),
                    WallSize = Model.Divider.Model.WallSize,
                    BottomCutoutSize = Model.Divider.Model.BottomCutoutSize
                });
                var translate = new Vector3(spaceBetween * (i + 1) + Model.Divider.Model.Size.X * i, -Model.Divider.Model.WallSize, 0);

                if (Model.Angle != 0)
                {
                    divider = divider.Rotate(0, Model.Angle, 0);
                    translate += new Vector3(Model.Divider.Model.Size.Y * Math.Cos(45), 0, 0);
                }
                
                trayObjects.Add(divider.Translate(translate));
            }
            var cube = boxComponent
                .Union(trayObjects.ToArray());
                /*.Union(
                    new HorizontalCutout(new Vector3(3, Model.Size.Y / 2, Model.Size.Z / 2 + Model.Size.Y / 4), 10)
                        .Translate(new Vector3(Model.Size.X, Model.Size.Y / 2, Model.Size.Z / 2)),
                    new HorizontalCutout(new Vector3(3, Model.Size.Y / 2, Model.Size.Z / 2 + Model.Size.Y / 4), 10)
                );*/
            return cube.ToString();
        }
    }
}
