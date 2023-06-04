using System.Collections.Generic;
using System.Windows.Controls;

namespace Nada.NZazu;

public interface INZazuWpfLayoutStrategy
{
    /// <summary>
    ///     Calls the layout method on the content control. This is the method that will be called when the user clicks on a
    ///     field or the layout is changed
    /// </summary>
    /// <param name="contentControl">The control on which to do the layout</param>
    /// <param name="fields">The fields that are being laid out</param>
    /// <param name="resolveLayout">A callback to determine the layout to use</param>
    void DoLayout(ContentControl contentControl, IEnumerable<INZazuWpfField> fields,
        IResolveLayout resolveLayout = null!);
}