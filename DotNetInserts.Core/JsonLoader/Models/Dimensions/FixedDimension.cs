using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Models.Dimensions
{
    public class FixedDimension : IDimension
    {
        public required double Size { get; set; }

        public double GetSize(JsonInsertContext context)
        {
            return Size;
        }
    }
}
