namespace Nada.Core.JStore;

/// <summary>
///     Interface to read files from e.g. a file system.
/// </summary>
public interface IFileStore
{
    /// <summary>
    /// </summary>
    /// <param name="path">Path to the folder of the files</param>
    string Read(string path);

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <param name="source"></param>
    void Save(string path, string source);
}