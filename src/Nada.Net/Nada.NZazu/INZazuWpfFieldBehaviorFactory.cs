using Nada.NZazu.Contracts;

namespace Nada.NZazu
{
    public interface INZazuWpfFieldBehaviorFactory
    {
        INZazuWpfFieldBehavior CreateFieldBehavior(BehaviorDefinition behaviorDefinition);
    }
}