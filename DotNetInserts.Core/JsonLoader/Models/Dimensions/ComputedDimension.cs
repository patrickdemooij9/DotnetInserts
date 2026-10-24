using System.Globalization;
using System.Text.RegularExpressions;
using DotNetInserts.Core.JsonLoader.Models.Dimensions;
using Flee.PublicTypes;

public class ComputedDimension : IDimension
{
    /// <summary>
    /// Expressions in the insert JSON files use ',' as the decimal separator
    /// ("68+1,2"). Flee otherwise takes its separators from the ambient culture, which
    /// meant those files only parsed on a machine with a comma locale - they would fail
    /// on en-US, on CI, or in a container. Pinning the separators here makes the same
    /// JSON evaluate identically everywhere.
    /// </summary>
    public const char DecimalSeparator = ',';

    public const char ArgumentSeparator = ';';

    private static readonly NumberFormatInfo ExpressionNumberFormat = new()
    {
        NumberDecimalSeparator = DecimalSeparator.ToString(),
        NumberGroupSeparator = string.Empty
    };

    public required string Expression { get; set; }

    public double GetSize(JsonInsertContext context)
    {
        // "#Part.Component.X", or "#{Part name.Component name.X}" when a name holds
        // anything other than word characters - the default "New Component" has a space.
        var replaced = Regex.Replace(Expression, @"#\{([^}]+)\}|#([\w\.]+)", match =>
        {
            var variable = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value;
            var splitPart = variable.Split('.');
            if (splitPart.Length < 2)
                throw new InvalidOperationException($"Invalid part format '#{variable}'. Expected format 'partName.property'.");

            var part = context.GetPart(splitPart[0]) as IExposeVariables
                ?? throw new InvalidOperationException($"Part '{splitPart[0]}' not found in context.");

            if (part.GetVariables(context).TryGetValue(variable, out var value) && value is IDimension size)
                // Must match the separator the parser below is pinned to, since this
                // number is substituted back into the expression text.
                return size.GetSize(context).ToString(ExpressionNumberFormat);

            throw new InvalidOperationException($"Property '{splitPart[1]}' not found in part '{splitPart[0]}'.");
        });

        var expr = new ExpressionContext();
        expr.ParserOptions.DecimalSeparator = DecimalSeparator;
        expr.ParserOptions.FunctionArgumentSeparator = ArgumentSeparator;
        expr.ParserOptions.RecreateParser();

        var result = expr.CompileGeneric<double>(replaced).Evaluate();
        return result;
    }
}
