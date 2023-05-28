using System.Globalization;
using System.Reflection;
using Nada.Core.Replacer.Handlers;

namespace Nada.Core.Replacer;

public class PropertyParserFactory : IPropertyParserFactory
{
    public IPropertyParser Create(CultureInfo cultureInfo, IEnumerable<ITokenTypeHandler>? additionalHandlers = null)
    {
        if (cultureInfo == null) throw new ArgumentNullException(nameof(cultureInfo));

        var handlers = new List<ITokenTypeHandler>(additionalHandlers ?? Enumerable.Empty<ITokenTypeHandler>());

        var internalHandlers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(ITokenTypeHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract &&
                        (t.IsPublic || t.IsNotPublic))
            .Select(type => InstantiateTokenTypeHandler(type, cultureInfo));

        handlers.AddRange(internalHandlers);
        return new PropertyParser(handlers);
    }

    public IPropertyParser Create()
    {
        var handlers = new List<ITokenTypeHandler>();

        var internalHandlers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(ITokenTypeHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract &&
                        (t.IsPublic || t.IsNotPublic))
            .Select(type => InstantiateTokenTypeHandler(type, CultureInfo.CurrentUICulture));

        handlers.AddRange(internalHandlers);
        return new PropertyParser(handlers);
    }

    private static ITokenTypeHandler InstantiateTokenTypeHandler(Type type, CultureInfo cultureInfo)
    {
        if (type.IsAbstract) throw new ArgumentException($"Type {type} cannot be abstract");

        var constructor = type.GetConstructor(new[] { typeof(CultureInfo) });

        if (constructor != null)
            // Invoke constructor with CultureInfo parameter
            return (ITokenTypeHandler)constructor.Invoke(new[] { cultureInfo });

        constructor = type.GetConstructor(Array.Empty<Type>()) ?? throw new MissingMemberException(
            $"There is no constructor which only accepts CultureInfo or has empty parameters in type {type}");
        return (ITokenTypeHandler)constructor.Invoke(Array.Empty<object>());
    }
}