using System.Globalization;

namespace Nada.Replacer.Handlers;

internal class DateTimeTokenTypeHandler : ITokenTypeHandler
{
    private readonly CultureInfo _cultureInfo;

    public DateTimeTokenTypeHandler(CultureInfo cultureInfo)
    {
        _cultureInfo = cultureInfo;
    }

    public bool CanHandle(string dataType)
    {
        return string.Equals(dataType, "datetime", StringComparison.OrdinalIgnoreCase);
    }

    public TokenHandlerResult Handle(string key, string value, string additionalInformation,
        IDictionary<string, string>? @params)
    {
        if (string.IsNullOrWhiteSpace(value) || !DateTime.TryParseExact(value, "o", _cultureInfo,
                DateTimeStyles.AssumeUniversal, out var dateTimeValue)) return new TokenHandlerResult("", false);

        dateTimeValue = dateTimeValue.ToLocalTime();

        if (string.IsNullOrWhiteSpace(additionalInformation))
            return new TokenHandlerResult(dateTimeValue.ToString(_cultureInfo));

        return new TokenHandlerResult(additionalInformation switch
        {
            _ => dateTimeValue.ToString(additionalInformation, _cultureInfo)
        });
    }
}