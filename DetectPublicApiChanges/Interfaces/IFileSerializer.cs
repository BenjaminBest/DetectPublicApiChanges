using System.IO;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// Interface for a serializer
    /// </summary>
    public interface IFileSerializer
    {
        /// <summary>
        /// Serializes the specified file.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        void Serialize<TType>(FileInfo file, TType data);

        /// <summary>
        /// Deserializes the specified file.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        TType Deserialize<TType>(FileInfo file);
    }
}