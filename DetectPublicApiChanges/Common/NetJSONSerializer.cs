using System.IO;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// A proto buf based serializer
    /// </summary>
    /// <seealso cref="IFileSerializer" />
    public class NetJsonSerializer : IFileSerializer
    {
        /// <summary>
        /// Serializes the given <paramref name="data" /> to the file <paramref name="targetFile" />
        /// </summary>
        /// <param name="targetFile">The target file</param>
        /// <param name="data">The data which will be serialized</param>
        public void Serialize<TType>(FileInfo targetFile, TType data)
        {
            using (TextWriter file = File.CreateText(targetFile.FullName))
            {
                NetJSON.NetJSON.Serialize(data, file);
            }
        }

        /// <summary>
        /// Deserializes the data in the given <paramref name="sourceFile"/> as an object with type <typeparamref name="TType"/>
        /// </summary>
        /// <typeparam name="TType">The type</typeparam>
        /// <param name="sourceFile">The file to read from</param>
        /// <returns>Instance of type <typeparamref name="TType"/></returns>
        public TType Deserialize<TType>(FileInfo sourceFile)
        {
            TType result;

            if (!sourceFile.Exists)
                throw new FileNotFoundException($"File '{sourceFile}' does not exists.");

            using (TextReader file = File.OpenText(sourceFile.FullName))
            {
                result = NetJSON.NetJSON.Deserialize<TType>(file);
            }

            return result;
        }
    }
}
