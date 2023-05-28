using System.Diagnostics.CodeAnalysis;

namespace Nada.JStore;

/// <summary>
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Cannot be tested in an isolated test")]
public class FileStore : IFileStore
{
    private readonly string _root;

    /// <summary>
    /// </summary>
    public FileStore(string root)
    {
        _root = root;
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string Read(string path)
    {
        return File.ReadAllText(Path.Combine(_root, path));
    }

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="source"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Save(string path, string source)
    {
        File.WriteAllText(Path.Combine(_root, path), source);
    }
}