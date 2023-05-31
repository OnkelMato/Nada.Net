using System.Globalization;

namespace Nada.Core.Replacer.Handlers;

internal class DateOnlyTokenTypeHandler : ITokenTypeHandler
{
    private readonly CultureInfo _cultureInfo;

    public DateOnlyTokenTypeHandler(CultureInfo cultureInfo)
    {
        _cultureInfo = cultureInfo;
    }

    public bool CanHandle(string dataType)
    {
        return string.Equals(dataType, "dateonly", StringComparison.OrdinalIgnoreCase) || string.Equals(dataType, "do", StringComparison.OrdinalIgnoreCase);
    }

    public TokenHandlerResult Handle(string key, string value, string additionalInformation,
        IDictionary<string, string>? @params)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            !DateOnly.TryParseExact(value, "o", _cultureInfo, DateTimeStyles.None, out var dateTimeValue))
            return new TokenHandlerResult("", false);


        if (string.IsNullOrWhiteSpace(additionalInformation))
            return new TokenHandlerResult(dateTimeValue.ToString(_cultureInfo));

        return new TokenHandlerResult(additionalInformation switch
        {
            _ => dateTimeValue.ToString(additionalInformation, _cultureInfo)
        });
    }
}