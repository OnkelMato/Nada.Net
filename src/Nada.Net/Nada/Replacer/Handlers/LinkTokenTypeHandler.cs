namespace Nada.Replacer.Handlers;

internal class LinkTokenTypeHandler : ITokenTypeHandler
{
    public bool CanHandle(string dataType)
    {
        return string.Equals(dataType, "link", StringComparison.OrdinalIgnoreCase) || string.Equals(dataType, "l", StringComparison.OrdinalIgnoreCase);
    }

    public TokenHandlerResult Handle(string key, string value, string additionalInformation,
        IDictionary<string, string>? @params)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(additionalInformation) || @params == null)
            return new TokenHandlerResult("", false);

        var title = additionalInformation[0] switch
        {
            '@' => @params.TryGetValue(additionalInformation[1..], out var t) ? t : "!@#)(*`",
            _ => additionalInformation
        };

        if ("!@#)(*`".Equals(title)) return new TokenHandlerResult("", false);

        return new TokenHandlerResult($"<a href='{value}'>{title}</a>");
    }
}