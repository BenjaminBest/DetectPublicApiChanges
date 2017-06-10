using System.Collections.Generic;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Analysis.StructureIndex
{
    /// <summary>
    /// The StructureIndex is used to save and retrieve efficiently index items
    /// </summary>
    /// <seealso cref="IStructureIndex" />
    public class StructureIndex : IStructureIndex
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// The items
        /// </summary>
        private readonly Dictionary<string, IIndexItem> _items = new Dictionary<string, IIndexItem>();

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IReadOnlyDictionary<string, IIndexItem> Items => _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureIndex"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public StructureIndex(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Adds the or update item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddOrUpdateItem(IIndexItem item)
        {
            var key = item.Key;

            if (_items.ContainsKey(key))
            {
                _logger.Warn($"Updated item with key '{key}' in index. This should never happen.");
                _items[key] = item;
                return;
            }

            _logger.Debug($"Add item with key '{key}' to index.");
            _items.Add(key, item);

        }

        /// <summary>
        /// Existses the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return _items.ContainsKey(key);
        }
    }
}
