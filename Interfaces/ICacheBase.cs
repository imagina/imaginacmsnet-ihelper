namespace Ihelpers.Interfaces
{
    /// <summary>
    /// The `ICacheBase` interface provides basic cache functionalities.
    /// </summary>
    public interface ICacheBase
    {
        /// <summary>
        /// Removes a value from the cache, based on its key.
        /// </summary>
        /// <typeparam name="T">The type of the value to be removed.</typeparam>
        /// <param name="key">The key of the value to be removed.</param>
        public void Remove<T>(object key);

        /// <summary>
        /// Creates a new value in the cache, with the given key and expiration time.
        /// </summary>
        /// <typeparam name="T">The type of the value to be created.</typeparam>
        /// <param name="key">The key of the value to be created.</param>
        /// <param name="value">The value to be created.</param>
        /// <param name="ExpirationTime">The optional expiration time for the value (in minutes).</param>
        /// <returns>The value that was created in the cache.</returns>
        public T CreateValue<T>(object key, T value, double? ExpirationTime = null);

        /// <summary>
        /// Creates a new value in the cache, with the given key and expiration time in minutes.
        /// </summary>
        /// <param name="key">The key of the value to be created.</param>
        /// <param name="value">The value to be created.</param>
        /// <param name="ExpirationTime">The optional expiration time for the value (in minutes).</param>
        /// <returns>The value that was created in the cache.</returns>
        public object CreateValue(object key, object value, double? ExpirationTime = null);

        /// <summary>
        /// Gets the value from the cache, based on its key.
        /// </summary>
        /// <param name="key">The key of the value to be retrieved.</param>
        /// <returns>The value that was retrieved from the cache, or `null` if the value was not found.</returns>
        public object? GetValue(object key);

        /// <summary>
        /// Gets the value from the cache, based on its key.
        /// </summary>
        /// <typeparam name="T">The type of the value to be retrieved.</typeparam>
        /// <param name="key">The key of the value to be retrieved.</param>
        /// <returns>The value that was retrieved from the cache, casted to the specified type.</returns>
        public T GetValue<T>(object key);
    }
}
