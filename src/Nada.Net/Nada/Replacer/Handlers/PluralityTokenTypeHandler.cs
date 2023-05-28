namespace Nada.Replacer.Handlers;

internal class PluralityTokenTypeHandler : ITokenTypeHandler
{
    public bool CanHandle(string dataType)
    {
        return string.Equals(dataType, "plurality", StringComparison.OrdinalIgnoreCase);
    }

    public TokenHandlerResult Handle(string key, string value, string additionalInformation,
        IDictionary<string, string>? @params)
    {
        if (string.IsNullOrWhiteSpace(value) || @params == null) return new TokenHandlerResult("", false);

        if (long.TryParse(value, out var longValue))
            return new TokenHandlerResult(SingularOrPlural(@params, additionalInformation, longValue));

        if (decimal.TryParse(value, out var decimalValue))
            return new TokenHandlerResult(SingularOrPlural(@params, additionalInformation, decimalValue));

        if (double.TryParse(value, out var doubleValue))
            return new TokenHandlerResult(SingularOrPlural(@params, additionalInformation, doubleValue));

        return new TokenHandlerResult("", false);
    }

    private static string SingularOrPlural(IDictionary<string, string> dict, string format, dynamic numberValue)
    {
        // Token = {plurality|Task|A|@Task}
        //  - format = "A|@Task"
        //  - numberValue is value in the dictionary for replacement
        //  - parts = "A", "@Task"

        // Token = {plurality|Task|has|have} 
        //  - format = "has|have"
        //  - parts = "has", "have"
        // 
        var parts = format.Split('|');

        var singular = parts[0][0] switch
        {
            '@' => dict.TryGetValue(parts[0][1..], out var value) ? value : parts[0],
            _ => parts[0]
        };

        var plural = parts[1][0] switch
        {
            '@' => dict.TryGetValue(parts[1][1..], out var t) ? t : parts[1],
            _ => parts[1]
        };

        return (numberValue == 1 ? singular : plural)!;
    }
}