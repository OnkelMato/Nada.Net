using System.Text;
using System.Text.RegularExpressions;
using Nada.Extensions;

namespace Nada.Replacer.Handlers;

internal class StringTokenTypeHandler : ITokenTypeHandler
{
    public bool CanHandle(string dataType)
    {
        return string.Equals(dataType, "string", StringComparison.OrdinalIgnoreCase);
    }

    public TokenHandlerResult Handle(string key, string value, string additionalInformation, IDictionary<string, string>? @params)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new("", false);
        }

        // no truncate
        if (string.IsNullOrWhiteSpace(additionalInformation))
        {
            return new(value);
        }

        // truncate
        var truncateInformations = additionalInformation.Split('|');

        // Check if a truncation length is provided
        if (!truncateInformations[0].All(char.IsDigit))
        {
            return new("", false);
        }

        var truncateLength = int.Parse(truncateInformations[0]);
        var truncateType = truncateInformations.Length > 1 ? 
            truncateInformations[1] : "plain";

        if(string.Equals(truncateType, "html", StringComparison.OrdinalIgnoreCase))
        {
            return new(TruncateHtml(value, truncateLength));
        }
        else
        {
            // Truncate plain with trailing "…"
            return new(value.Truncate(truncateLength));
        }
    }

    private static string TruncateHtml(string value, int length)
    {
        var regexPattern = "</?(?<tagName>\\w+).*?>";
        var regexMatches = Regex.Matches(value, regexPattern, RegexOptions.None, TimeSpan.FromMilliseconds(200)).Cast<Match>();

        var untagged = Regex.Replace(value, regexPattern, string.Empty, RegexOptions.None, TimeSpan.FromMilliseconds(200));
        var truncated = new StringBuilder(untagged.Truncate(length));

        var openTags = new List<string>();
        foreach (var match in regexMatches)
        {
            // Check if index is greater than truncated length without "…" char
            if (match.Index > truncated.Length - 1)
            {
                continue;
            }

            var isClosingTag = match.Value.StartsWith("</");
            var tagName = match.Groups["tagName"].Value;

            if (isClosingTag)
            {
                // Remove previously opened tag, if exists
                openTags.Remove(tagName);
            }
            else
            {
                openTags.Add(tagName);
            }

            truncated.Insert(match.Index, match.Value);
        }

        // Close all previously opened tags in reverse order
        for(int i = openTags.Count - 1; i >= 0; i--)
        {
            truncated.Append($"</{openTags[i]}>");
        }

        return truncated.ToString();
    }
}