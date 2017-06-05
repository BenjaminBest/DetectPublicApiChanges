using System.Collections.Generic;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// The Store is used to save configuration values at a central place so thate multiple steps can use them
    /// </summary>
    /// <seealso cref="IStore" />
    public class Store : IStore
    {
        /// <summary>
        /// The store
        /// </summary>
        private readonly Dictionary<string, object> _store = new Dictionary<string, object>();

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Store"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public Store(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool GetItem(string key, out object item)
        {
            if (!_store.ContainsKey(key))
            {
                item = null;
                return false;
            }

            item = _store[key];
            return true;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object GetItem(string key)
        {
            return !_store.ContainsKey(key) ? null : _store[key];
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetItem<T>(string key)
        {
            return !_store.ContainsKey(key) ? default(T) : (T)_store[key];
        }

        /// <summary>
        /// Sets the or add item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        public void SetOrAddItem(string key, object item)
        {
            if (!_store.ContainsKey(key))
            {
                _store.Add(key, item);
                _logger.Debug($"Add item with key '{key}'");
            }
            else
            {
                _store[key] = item;
                _logger.Debug($"Set item with key '{key}'");
            }
        }

        /// <summary>
        /// Determines whether the specified key has item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the specified key has item; otherwise, <c>false</c>.
        /// </returns>
        public bool HasItem(string key)
        {
            return _store.ContainsKey(key);
        }
    }
}