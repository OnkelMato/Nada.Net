using System.Collections.Generic;

namespace Nada.NZazu.Contracts.Suggest;

public interface IProvideSuggestions
{
    IEnumerable<string> For(string prefix, string dataconnection);
}