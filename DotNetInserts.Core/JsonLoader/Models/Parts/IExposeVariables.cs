using DotNetInserts.Core.JsonLoader.Models.Dimensions;

public interface IExposeVariables
{
    Dictionary<string, IDimension> GetVariables(JsonInsertContext context);
}