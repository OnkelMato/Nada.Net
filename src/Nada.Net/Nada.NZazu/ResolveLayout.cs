using System.Collections.Generic;
using Nada.NZazu.LayoutStrategy;

namespace Nada.NZazu;

public class ResolveLayout : IResolveLayout
{
    private static readonly INZazuWpfLayoutStrategy Default = new GridLayout();

    private readonly Dictionary<string, INZazuWpfLayoutStrategy> _layouts =
        new()
        {
            { "stack", new StackedLayout() }
        };

    public INZazuWpfLayoutStrategy Resolve(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return Default;
        return _layouts.TryGetValue(name, out var layout) ? layout : Default;
    }
}