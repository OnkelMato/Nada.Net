namespace Nada.Core.Replacer.Handlers;

internal class GuidTokenTypeHandler : ITokenTypeHandler
{
    public bool CanHandle(string dataType)
    {
        return string.Equals(dataType, "guid", StringComparison.OrdinalIgnoreCase) || string.Equals(dataType, "g", StringComparison.OrdinalIgnoreCase);
    }

    public TokenHandlerResult Handle(string key, string value, string additionalInformation,
        IDictionary<string, string>? @params)
    {
        if (string.IsNullOrWhiteSpace(value) || !Guid.TryParse(value, out var guidValue))
            return new TokenHandlerResult("", false);

        if (string.IsNullOrWhiteSpace(additionalInformation)) return new TokenHandlerResult(guidValue.ToString());

        return new TokenHandlerResult(guidValue.ToString(additionalInformation));
    }
}