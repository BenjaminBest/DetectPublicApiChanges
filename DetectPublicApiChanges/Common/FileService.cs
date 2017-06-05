using System.Collections.Generic;
using System.IO;
using System.Linq;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// The FileService abstracts the access of files
    /// </summary>
    /// <seealso cref="IFileService" />
    public class FileService : IFileService
    {
        /// <summary>
        /// Gets the file names in directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public IEnumerable<string> GetFileNamesInDirectory(DirectoryInfo directory, string searchPattern)
        {
            return directory.GetFiles(searchPattern).Select(f => f.Name);
        }

        /// <summary>
        /// Writes the given <paramref name="text"/>
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="text">The text.</param>
        public void WriteAllText(string fileName, string text)
        {
            File.WriteAllText(fileName, text);
        }
    }
}
