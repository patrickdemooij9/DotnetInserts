using OSCADSharp;

namespace DotNetInserts.Core.CustomBlocks
{
    public interface IPartWithExtraParts
    {
        OSCADObject[] GetExtraParts();
    }
}