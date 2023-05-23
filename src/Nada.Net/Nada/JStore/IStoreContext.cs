namespace Nada.JStore;

/// <summary>
///     Interface to convert a data source to objects
/// </summary>
public interface IStoreContext
{
    /// <summary>
    ///     Returns a list of <see cref="TClass" /> objects
    /// </summary>
    IEnumerable<TClass> Get<TClass>() where TClass : class;
}