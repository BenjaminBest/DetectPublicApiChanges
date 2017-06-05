using System.Collections.Generic;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IStructureIndex defines an interleaved index for structured index items.
    /// </summary>
    public interface IStructureIndex
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IReadOnlyDictionary<string, IStructureIndexItem> Items { get; }

        /// <summary>
        /// Adds the or update item.
        /// </summary>
        /// <param name="item">The item.</param>
        void AddOrUpdateItem(IStructureIndexItem item);

        /// <summary>
        /// Existses the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool Exists(string key);
    }
}