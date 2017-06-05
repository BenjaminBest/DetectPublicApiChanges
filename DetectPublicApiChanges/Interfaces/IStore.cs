namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IStore defines a configuration repository
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        bool GetItem(string key, out object item);

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object GetItem(string key);

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetItem<T>(string key);

        /// <summary>
        /// Sets the or add item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        void SetOrAddItem(string key, object item);

        /// <summary>
        /// Determines whether the specified key has item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the specified key has item; otherwise, <c>false</c>.
        /// </returns>
        bool HasItem(string key);

    }
}