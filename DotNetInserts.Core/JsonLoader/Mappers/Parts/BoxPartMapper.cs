using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.JsonLoader.Mappers.Components;
using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.JsonLoader.Models.Parts;
using DotNetInserts.Core.Models;
using OSCADSharp;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Mappers.Parts
{
    public class BoxPartMapper : IPartMapper
    {
        public Type PartType => typeof(BoxPartModel);

        private readonly IComponentMapper[] _mappers = [new BoxComponentMapper(), new RepeatableComponentMapper(), new CircleComponentMapper(), new CardTrayComponentMapper(), new SvgComponentMapper()];

        public OSCADObject Map(IPart part, JsonInsertContext context)
        {
            var boxPart = (BoxPartModel)part;
            var size = part.Size.ToVector3(context);

            var insertBox = new InsertBox(size, boxPart.RoundRadius);
            foreach (var label in boxPart.Labels)
            {
                insertBox.Labels.Add(label.ToDataModel());
            }
            foreach (var component in boxPart.Components)
            {
                var mapper = _mappers.FirstOrDefault(m => m.ComponentType == component.GetType()) ?? throw new InvalidOperationException("No mapper found");

                context.CurrentComponent = component;
                var location = new Vector3(
                                    component.Location.X.GetSize(context),
                                    component.Location.Y.GetSize(context),
                                    component.Location.Z.GetSize(context));

                insertBox.Components.Add(mapper.Map(component, context), new ComponentLocation(location));
            }
            if (boxPart.Lid != null)
            {
                var lid = new InsertBoxLidDataModel(boxPart.Lid.WallSize, boxPart.Lid.TopSize)
                {
                    LidBoxSize = boxPart.Lid.LidBoxSize,
                    SpaceUnderLid = boxPart.Lid.SpaceUnderLid,
                    //ImagePath = "20Strong.svg"
                    SkirtThickness = boxPart.Lid.SkirtThickness,
                    Clearance = boxPart.Lid.Clearance,
                    Snap = boxPart.Lid.Snap?.ToDataModel(),
                    Grip = boxPart.Lid.Grip?.ToDataModel(),
                    OwnerName = boxPart.Name
                };
                if (boxPart.Lid.MinWall.HasValue) lid.MinWall = boxPart.Lid.MinWall.Value;

                insertBox.Lid = lid;

                insertBox.GetLid().WithTolerance().ToFile("lid_preview.scad");
            }

            return insertBox;
        }
    }
}
