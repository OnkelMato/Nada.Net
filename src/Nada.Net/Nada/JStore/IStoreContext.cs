namespace Nada.JStore;

/// <summary>
/// Interface to convert a data source to objects and write object back to the data store
/// </summary>
public interface IStoreContext
{
    /// <summary>
    /// Returns a list of <typeparam name="TClass" /> objects
    /// </summary>
    IEnumerable<TClass> Get<TClass>() where TClass : class;

    /// <summary>
    /// Stores a list of <typeparam name="TClass" /> objects
    /// </summary>
    void Save<TClass>(IEnumerable<TClass> data);
}