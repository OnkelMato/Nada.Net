namespace Nada.NZazu
{
    public interface IResolveFromName<out T>
    {
        T Resolve(string name);
    }
}