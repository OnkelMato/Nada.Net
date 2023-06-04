using System.Globalization;

namespace Nada.Core.Replacer.Handlers;

internal class TimeOnlyTokenTypeHandler : ITokenTypeHandler
{
    private readonly CultureInfo _cultureInfo;

    public TimeOnlyTokenTypeHandler(CultureInfo cultureInfo)
    {
        _cultureInfo = cultureInfo;
    }

    public bool CanHandle(string dataType)
    {
        return string.Equals(dataType, "timeonly", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(dataType, "to", StringComparison.OrdinalIgnoreCase);
    }

    public TokenHandlerResult Handle(string key, string value, string additionalInformation,
        IDictionary<string, string>? @params)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            !TimeOnly.TryParseExact(value, "o", _cultureInfo, DateTimeStyles.None, out var dateTimeValue))
            return new TokenHandlerResult("", false);


        if (string.IsNullOrWhiteSpace(additionalInformation))
            return new TokenHandlerResult(dateTimeValue.ToString(_cultureInfo));

        return new TokenHandlerResult(additionalInformation switch
        {
            _ => dateTimeValue.ToString(additionalInformation, _cultureInfo)
        });
    }
}