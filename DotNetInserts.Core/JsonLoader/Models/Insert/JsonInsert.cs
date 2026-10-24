using DotNetInserts.Core.JsonLoader.Models.Parts;
using OSCADSharp.Spatial;

namespace DotNetInserts.Core.JsonLoader.Models.Insert
{
    public class JsonInsert
    {
        public required string Name { get; set; }
        public required Vector3 Size { get; set; }
        public IPart[] Parts { get; set; }

        public JsonInsert()
        {
            Parts = [];
        }
    }
}
