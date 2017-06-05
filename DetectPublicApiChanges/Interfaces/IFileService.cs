using System.Collections.Generic;
using System.IO;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IFileService defines a service to abstract the access of files.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Gets the file names in directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        IEnumerable<string> GetFileNamesInDirectory(DirectoryInfo directory, string searchPattern);

        /// <summary>
        /// Writes the given <paramref name="text"/>
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="text">The text.</param>
        void WriteAllText(string fileName, string text);
    }
}