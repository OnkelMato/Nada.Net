using System.Text.Json;

namespace Nada.JStore;

/// <summary>
///     Implementation for a store based on json and files
/// </summary>
public class JsonFileStoreContext : IStoreContext
{
    private readonly IFileStore _fileStore;

    /// <summary>
    ///     Creates a new instance of <see cref="JsonFileStoreContext" />
    /// </summary>
    /// <param name="fileStore">Instance of a file reader</param>
    public JsonFileStoreContext(IFileStore fileStore)
    {
        _fileStore = fileStore;
    }

    /// <summary>
    ///     Returns a list of <see cref="TClass" /> objects
    /// </summary>
    public IEnumerable<TClass>? Get<TClass>() where TClass : class
    {
        var filename = typeof(TClass).Name + ".json";
        var source = _fileStore.Read(filename);

        return JsonSerializer.Deserialize<IEnumerable<TClass>>(source);
    }

    public void Save<TClass>(IEnumerable<TClass> data)
    {
        var filename = typeof(TClass).Name + ".json";

        var source = JsonSerializer.Serialize(data);

        _fileStore.Save(filename, source);
    }
}