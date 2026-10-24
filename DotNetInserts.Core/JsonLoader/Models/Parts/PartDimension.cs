using DotNetInserts.Core.JsonLoader.Models.Dimensions;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Models.Parts
{
    public class PartDimension
    {
        public required IDimension X { get; set; }
        public required IDimension Y { get; set; }
        public required IDimension Z { get; set; }

        public Vector3 ToVector3(JsonInsertContext context)
        {
            return new Vector3(
                X.GetSize(context),
                Y.GetSize(context),
                Z.GetSize(context)
            );
        }

        public static PartDimension ZeroDimension() => new()
        {
            X = new FixedDimension() { Size = 0 },
            Y = new FixedDimension() { Size = 0 },
            Z = new FixedDimension() { Size = 0 }
        };
    }
}
