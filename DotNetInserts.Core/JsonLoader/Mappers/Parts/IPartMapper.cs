using DotNetInserts.Core.JsonLoader.Models.Parts;
using OSCADSharp;

namespace DotNetInserts.Core.JsonLoader.Mappers.Parts
{
    public interface IPartMapper
    {
        Type PartType { get; }

        OSCADObject Map(IPart part, JsonInsertContext context);
    }
}
