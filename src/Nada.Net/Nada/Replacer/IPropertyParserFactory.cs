using System.Globalization;
using Nada.Replacer.Handlers;

namespace Nada.Replacer;

public interface IPropertyParserFactory
{
    /// <summary>
    ///     Create an IPropertyParser instance with the given CultureInfo and an optional list of additional
    ///     <see cref="ITokenTypeHandler" /> objects
    /// </summary>
    /// <param name="cultureInfo">
    ///     It is used when formatting <see cref="DateTime" />, <see cref="DateOnly" /> and
    ///     <see cref="TimeOnly" /> types.
    /// </param>
    /// <param name="additionalHandlers">It is used to override existing or add new <see cref="ITokenTypeHandler" /> objects</param>
    /// <returns>A new IPropertyParser implementation</returns>
    IPropertyParser Create(CultureInfo cultureInfo, IEnumerable<ITokenTypeHandler>? additionalHandlers = null);

    /// <summary>
    ///     Create an IPropertyParser instance with the given CultureInfo and an optional list of additional
    ///     <see cref="ITokenTypeHandler" /> types
    /// </summary>
    /// <param name="cultureInfo">
    ///     It is used when formatting <see cref="DateTime" />, <see cref="DateOnly" /> and
    ///     <see cref="TimeOnly" /> types.
    /// </param>
    /// <param name="additionalHandlers">It is used to override existing or add new <see cref="ITokenTypeHandler" /> types</param>
    /// <returns>A new IPropertyParser implementation</returns>
    IPropertyParser Create(CultureInfo cultureInfo, IEnumerable<Type> additionalHandlers);
}