using System.Text.Json;

namespace Nada.JStore;

/// <summary>
///     Implementation for a store based on json and files
/// </summary>
public class JsonFileStoreContext : IStoreContext
{
    private readonly IFileReader _fileReader;

    /// <summary>
    ///     Creates a new instance of <see cref="JsonFileStoreContext" />
    /// </summary>
    /// <param name="fileReader">Instance of a file reader</param>
    public JsonFileStoreContext(IFileReader fileReader)
    {
        _fileReader = fileReader;
    }

    /// <summary>
    ///     Returns a list of <see cref="TClass" /> objects
    /// </summary>
    public IEnumerable<TClass> Get<TClass>() where TClass : class
    {
        var filename = typeof(TClass).Name + ".json";
        var source = _fileReader.Read(filename);

        return JsonSerializer.Deserialize<IEnumerable<TClass>>(source) ?? throw new Exception();
    }

    public void Save<TClass>(IEnumerable<TClass> data)
    {
        var filename = typeof(TClass).Name + ".json";

        var source = JsonSerializer.Serialize<IEnumerable<TClass>>(data) ?? throw new Exception();

        _fileReader.Save(filename, source);
    }
}