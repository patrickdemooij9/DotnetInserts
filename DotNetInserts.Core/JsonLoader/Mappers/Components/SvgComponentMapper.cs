using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.Models;

namespace DotNetInserts.Core.JsonLoader.Mappers.Components
{
    public class SvgComponentMapper : IComponentMapper
    {
        public Type ComponentType => typeof(SvgComponentModel);

        public IComponent Map(IComponentModel model, JsonInsertContext context)
        {
            var svgModel = (SvgComponentModel)model;

            if (string.IsNullOrWhiteSpace(svgModel.ImagePath))
            {
                // Without a file the import renders as nothing, and a silently missing
                // engraving looks exactly like one the slicer swallowed.
                throw new InvalidOperationException(
                    $"svg component '{svgModel.Name}' has no imagePath.");
            }

            return new SvgComponent(new SvgComponentDataModel(
                svgModel.Size.ToVector3(context),
                svgModel.ImagePath,
                svgModel.Anchor,
                svgModel.Mirrored,
                svgModel.Export));
        }
    }
}
