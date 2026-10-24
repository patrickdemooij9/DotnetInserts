using DotNetInserts.Core.CustomBlocks;
using DotNetInserts.Core.JsonLoader.Models.Components;
using OSCADSharp;

namespace DotNetInserts.Core.JsonLoader.Mappers.Components
{
    public interface IComponentMapper
    {
        Type ComponentType { get; }

        IComponent Map(IComponentModel model, JsonInsertContext context);
    }
}
