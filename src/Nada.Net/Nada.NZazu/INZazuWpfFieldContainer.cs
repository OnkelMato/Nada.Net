using System.Collections.Generic;

namespace Nada.NZazu;

public interface INZazuWpfFieldContainer : INZazuWpfField
{
    IEnumerable<INZazuWpfField> Fields { get; set; }
    string Layout { get; set; }
}