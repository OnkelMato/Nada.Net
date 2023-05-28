using System.Text.RegularExpressions;
using Nada.Replacer.Handlers;

namespace Nada.Replacer;

public class PropertyParser : IPropertyParser
{
    public PropertyParser(IEnumerable<ITokenTypeHandler>? tokenTypeHandlers)
    {
        TokenTypeHandlers = tokenTypeHandlers ?? Enumerable.Empty<ITokenTypeHandler>();
    }

    public IEnumerable<ITokenTypeHandler> TokenTypeHandlers { get; }

    public string Parse(string template, IDictionary<string, string>? @params)
    {
        if (string.IsNullOrWhiteSpace(template) || @params == null || @params.Count == 0) return template;

        // Regex pattern to match tokens in the template
        var pattern =
            @"\{(?<dataType>\s*\w+\s*)(?:\|(?<key>\s*[\w_-]+\s*))?(?:\|(?<additionalInformation>\s*[^}]+\s*))?}";

        // Replace tokens in the template with corresponding values from the dictionary
        var result = Regex.Replace(template, pattern, match =>
        {
            var key = match.Groups["key"].Value.Trim();
            var additionalInformation = match.Groups["additionalInformation"].Value.Trim();
            var dataType = match.Groups["dataType"].Value.Trim().ToLowerInvariant();

            // token we would like to replace does not exist in @params dictionary
            // just don't do nothing and leave the token untouched
            if (!@params.TryGetValue(key, out var value)) return match.Value;

            // search appropriate handler, the first one wins
            // leave the token untouched if no handler is found
            var handler = TokenTypeHandlers.FirstOrDefault(x => x.CanHandle(dataType));
            if (handler == null) return match.Value;

            try
            {
                var result = handler.Handle(key, value, additionalInformation, @params);
                if (result.IsSuccess) return result.Value;

                return match.Value;
            }
            catch
            {
                return match.Value;
            }
        }, RegexOptions.None, TimeSpan.FromMilliseconds(200));

        return result;
    }
}