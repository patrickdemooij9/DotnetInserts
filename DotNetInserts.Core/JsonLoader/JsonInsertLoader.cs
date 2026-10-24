using DotNetInserts.Core.JsonLoader.Mappers.Parts;
using DotNetInserts.Core.JsonLoader.Models.Insert;
using OSCADSharp.Spatial;
using System.Text.Json;

namespace DotNetInserts.Core.JsonLoader
{
    public class JsonInsertLoader
    {
        private readonly IPartMapper[] _mappers = [new BoxPartMapper(), new CardHolderPartMapper()];

        public Insert Load(string filePath)
        {
            var file = File.ReadAllText(filePath);
            var insert = JsonSerializer.Deserialize<JsonInsert>(file) ?? throw new InvalidOperationException("Could not parse JSON");
            return Load(insert);
        }

        public Insert Load(JsonInsert insert)
        {
            var actualInsert = new Insert(insert.Name);
            var context = new JsonInsertContext(insert, insert.Parts[0]);
            foreach (var part in insert.Parts)
            {
                context.CurrentPart = part;
                var mapper = _mappers.FirstOrDefault(it => it.PartType == part.GetType()) ?? throw new InvalidOperationException("Could not find part mapper");

                if (part.Hidden)
                    continue;

                var location = part.Location.ToVector3(context);
                actualInsert.Parts.Add(new InsertPart(mapper.Map(part, context), location));
            }
            return actualInsert;
        }
    }
}
