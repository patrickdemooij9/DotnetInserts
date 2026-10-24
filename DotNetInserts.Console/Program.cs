using DotNetInserts.Core.JsonLoader.Models.Insert;
using DotNetInserts.Projects.AgentAvenue;
using DotNetInserts.Projects.Cascadia;
using DotNetInserts.Projects.Endangered;
using DotNetInserts.Projects.KingOfTokyoDuel;
using DotNetInserts.Projects.Sequence;
using DotNetInserts.Projects.Shatterpoint;
using DotNetInserts.Projects.SkyTeam;
using DotNetInserts.Projects.Talisman;
using DotNetInserts.Projects.TcgStorage;
using DotNetInserts.Projects.Test;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;

//new CardGameBox().Render();
/*var Size = new Vector3(2, Constants.CardSizeSleevedX, Constants.CardSizeSleevedZ);
new HorizontalCutout(new Vector3(Size.X, 10, 20), 5)
                        .Translate(0, Size.Y, Size.Z / 2).ToFile("Divider.scad");*/
//new InsertBoxTest().Render();

//new SkyTeamBox().Render();
new InsertJsonTest().Render();

/*JsonSerializerOptions options = JsonSerializerOptions.Default;
JsonNode schema = options.GetJsonSchemaAsNode(typeof(JsonInsert));
File.WriteAllText("jsonSchema.json", schema.ToString());*/

Console.WriteLine("Rendered!");
Console.ReadKey();