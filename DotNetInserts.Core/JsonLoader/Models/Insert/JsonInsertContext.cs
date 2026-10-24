using DotNetInserts.Core.JsonLoader.Models.Components;
using DotNetInserts.Core.JsonLoader.Models.Insert;
using DotNetInserts.Core.JsonLoader.Models.Parts;
using OSCADSharp.Spatial;

public class JsonInsertContext
{
    public JsonInsert Insert { get; }
    public IPart CurrentPart { get; set; }
    public IComponentModel? CurrentComponent { get; set; }

    public bool PreventInfiniteLoop { get; set; }

    /// <summary>
    /// Components whose componentAlign location is being worked out, so a chain that
    /// loops back on itself fails with a message instead of overflowing the stack.
    /// </summary>
    public HashSet<string> ResolvingComponents { get; } = [];

    /// <summary>The same guard for partAlign locations.</summary>
    public HashSet<string> ResolvingParts { get; } = [];

    public JsonInsertContext(JsonInsert insert, IPart currentPart)
    {
        Insert = insert;
        CurrentPart = currentPart;
    }

    public IPart GetPart(string name)
    {
        var part = Insert.Parts.FirstOrDefault(p => p.Name == name) ?? throw new ArgumentException($"Part with name '{name}' not found in the insert.");
        return part;
    }

    /// <summary>A component of the part being mapped, by id.</summary>
    public IComponentModel GetComponent(string id)
    {
        var components = (CurrentPart as BoxPartModel)?.Components
            ?? throw new InvalidOperationException($"Part '{CurrentPart.Name}' has no components to align to.");

        return components.FirstOrDefault(c => c.Id == id)
            ?? throw new ArgumentException($"Component with ID '{id}' not found in part '{CurrentPart.Name}'.");
    }

    /// <summary>
    /// Evaluates with <paramref name="component"/> as the current component, for
    /// dimensions like parentAlign that measure against whichever component is current.
    /// </summary>
    public T WithComponent<T>(IComponentModel component, Func<T> evaluate)
    {
        var previous = CurrentComponent;
        CurrentComponent = component;
        try
        {
            return evaluate();
        }
        finally
        {
            CurrentComponent = previous;
        }
    }

    /// <summary>
    /// Evaluates with <paramref name="part"/> as the current part, so a partAlign in the
    /// target's own location resolves against the target rather than the caller.
    /// </summary>
    public T WithPart<T>(IPart part, Func<T> evaluate)
    {
        var previous = CurrentPart;
        CurrentPart = part;
        try
        {
            return evaluate();
        }
        finally
        {
            CurrentPart = previous;
        }
    }
}