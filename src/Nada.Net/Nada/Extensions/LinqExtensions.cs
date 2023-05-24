namespace Nada.Extensions;

public static class LinqExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return source == null || !source.Any();
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        // ReSharper disable PossibleMultipleEnumeration
        if (source.IsNullOrEmpty()) return source;

        // we use a to array so it generated a copy and
        foreach (var item in source) action(item);

        return source;
        // ReSharper restore PossibleMultipleEnumeration
    }
}