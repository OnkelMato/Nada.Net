using System;
using Nada.NZazu.Contracts;
using Nada.NZazu.EventArgs;

namespace Nada.NZazu;

public interface INZazuWpfView : INZazuView
{
    INZazuWpfFieldFactory FieldFactory { get; set; }
    IResolveLayout ResolveLayout { get; set; }
    INZazuWpfField GetField(string key);
    bool TryGetField(string key, out INZazuWpfField field);
    event EventHandler<FieldFocusChangedEventArgs> FieldFocusChanged;
}