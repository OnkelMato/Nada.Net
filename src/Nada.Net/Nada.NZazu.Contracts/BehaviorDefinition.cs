using System.Collections.Generic;

namespace Nada.NZazu.Contracts
{
    public class BehaviorDefinition
    {
        public string Name { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}