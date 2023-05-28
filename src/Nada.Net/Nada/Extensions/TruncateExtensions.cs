using System.Diagnostics.CodeAnalysis;

namespace Nada.Core.Extensions;

/// <summary>
///     Allow strings to be truncated
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Copied from Humanizr project")]
public static class TruncateExtensions
{
    /// <summary>
    ///     Truncate the string
    /// </summary>
    /// <param name="input">The string to be truncated</param>
    /// <param name="length">The length to truncate to</param>
    /// <returns>The truncated string</returns>
    public static string Truncate(this string input, int length)
    {
        return input.Truncate(length, "…", new FixedLengthTruncator());
    }

    /// <summary>
    ///     Truncate the string
    /// </summary>
    /// <param name="input">The string to be truncated</param>
    /// <param name="length">The length to truncate to</param>
    /// <param name="truncator">The truncate to use</param>
    /// <param name="from">The enum value used to determine from where to truncate the string</param>
    /// <returns>The truncated string</returns>
    internal static string Truncate(this string input, int length, FixedLengthTruncator truncator,
        TruncateFrom from = TruncateFrom.Right)
    {
        return input.Truncate(length, "…", truncator, from);
    }

    /// <summary>
    ///     Truncate the string
    /// </summary>
    /// <param name="input">The string to be truncated</param>
    /// <param name="length">The length to truncate to</param>
    /// <param name="truncationString">The string used to truncate with</param>
    /// <param name="from">The enum value used to determine from where to truncate the string</param>
    /// <returns>The truncated string</returns>
    public static string Truncate(this string input, int length, string truncationString,
        TruncateFrom from = TruncateFrom.Right)
    {
        return input.Truncate(length, truncationString, new FixedLengthTruncator(), from);
    }

    /// <summary>
    ///     Truncate the string
    /// </summary>
    /// <param name="input">The string to be truncated</param>
    /// <param name="length">The length to truncate to</param>
    /// <param name="truncationString">The string used to truncate with</param>
    /// <param name="truncator">The truncator to use</param>
    /// <param name="from">The enum value used to determine from where to truncate the string</param>
    /// <returns>The truncated string</returns>
    internal static string Truncate(this string input, int length, string truncationString,
        FixedLengthTruncator truncator, TruncateFrom from = TruncateFrom.Right)
    {
        if (truncator == null) throw new ArgumentNullException(nameof(truncator));

        if (input == null) return null;

        return truncator.Truncate(input, length, truncationString, from);
    }
}