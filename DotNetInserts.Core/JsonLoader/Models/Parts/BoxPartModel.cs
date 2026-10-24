using DotNetInserts.Core.CustomBlocks.CustomCutouts;
using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.JsonLoader.Models.Dimensions;
using DotNetInserts.Core.JsonLoader.Models.Lids;

namespace DotNetInserts.Core.JsonLoader.Models.Parts
{
    public class BoxPartModel : IPart, IExposeVariables
    {
        public const string Type = "box";

        public required string Id { get; set; }
        public required string Name { get; set; }
        public bool Hidden { get; set; }
        public required PartDimension Size { get; set; }
        public required IComponentModel[] Components { get; set; }
        public LidDataModel? Lid { get; set; }
        public LabelModel[] Labels { get; set; } = [];

        public PartDimension Location { get; set; } = PartDimension.ZeroDimension();

        public double? RoundRadius { get; set; }

        public Dictionary<string, IDimension> GetVariables(JsonInsertContext context)
        {
            var variables = new Dictionary<string, IDimension>
            {
                { $"{Name}.X", Size.X },
                { $"{Name}.Y", Size.Y },
                { $"{Name}.Z", Size.Z }
            };
            // First name wins: a cloned component that has not been renamed yet must not
            // break every expression in the part.
            foreach (var component in Components)
            {
                variables.TryAdd($"{Name}.{component.Name}.X", component.Size.X);
                variables.TryAdd($"{Name}.{component.Name}.Y", component.Size.Y);
                variables.TryAdd($"{Name}.{component.Name}.Z", component.Size.Z);
            }
            return variables;
        }
    }
}
