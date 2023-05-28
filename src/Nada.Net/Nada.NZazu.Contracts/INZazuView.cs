using System.Collections.Generic;
using Nada.NZazu.Contracts.Checks;

namespace Nada.NZazu.Contracts
{
    public interface INZazuView
    {
        FormDefinition FormDefinition { get; set; }
        FormData FormData { get; set; }
        bool IsReadOnly { get; set; }
        bool TrySetFocusOn(string focusOn = null, bool force = false);
        Dictionary<string, string> GetFieldValues();
        void ApplyChanges();
        ValueCheckResult Validate();
    }
}