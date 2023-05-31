using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Nada.NZazu.Contracts.Suggest
{
    [ExcludeFromCodeCoverage]
    internal class FileSystem : IFileSystem
    {
        public bool FileExists(string file)
        {
            return new FileInfo(file).Exists;
        }

        public string[] ReadAllLines(string file)
        {
            return File.ReadAllLines(file);
        }
    }
}