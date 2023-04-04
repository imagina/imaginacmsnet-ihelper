using Ihelpers.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Ihelpers.Caching
{
    /// <summary>
    /// This class is a base class for memory cache implementation.
    /// It implements `ICacheBase` interface and uses `IMemoryCache` to store data.
    /// </summary>
    public class MemoryCacheBase : ICacheBase
    {
        /// <summary>
        /// An instance of `IMemoryCache` to store data.
        /// </summary>
        public IMemoryCache _cache;

        /// <summary>
        /// Cache entry options for cache data.
        /// </summary>
        private MemoryCacheEntryOptions cacheOptions;

        /// <summary>
        /// Constructor to initialize the cache.
        /// </summary>
        public MemoryCacheBase()
        {
            // Initialize cache options with a sliding expiration of 1 hour
            cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(1)
            };

            // Initialize an instance of `IMemoryCache`
            var cache = new MemoryCache(new MemoryCacheOptions());
            _cache = cache;

            // Set the cache instance in the `ConfigContainer` if not already set
            if (Ihelpers.Extensions.ConfigContainer.cache is null) Ihelpers.Extensions.ConfigContainer.cache = this;
        }

        /// <summary>
        /// Removes an item from cache based on its key.
        /// </summary>
        /// <typeparam name="T">Type of item stored in cache</typeparam>
        /// <param name="key">Key to identify the item in cache</param>
        public void Remove<T>(object key)
        {
            // Ensure that item exists to avoid exception throwing 
            object? internalValue = GetValueInternal<T>(key);

            // Remove the item if it exists in cache
            if (internalValue != null)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// Removes an item from cache based on its key.
        /// </summary>
        /// <param name="key">Key to identify the item in cache</param>
        public void Remove(object key)
        {
            // Ensure that item exists to avoid exception throwing 
            object? internalValue = GetValueInternal<object>(key);

            // Remove the item if it exists in cache
            if (internalValue != null)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// Tries to get a value from cache, or create it if not exists.
        /// </summary>
        /// <typeparam name="T">The type of the value to get or create.</typeparam>
        /// <param name="key">The key of the value to get or create.</param>
        /// <param name="value">The value to set in cache if not exists.</param>
        /// <returns>The value from cache.</returns>
        public T GetOrCreateValue<T>(object key, T value)
        {
            T internalValue = GetValueInternal<T>(key);

            if (internalValue == null)
            {
                _cache.Set<T>(key, value, cacheOptions);

                return value;
            }
            else
            {
                return internalValue;
            }
        }

        /// <summary>
        /// Creates a value in cache.
        /// </summary>
        /// <param name="key">The key of the value to create.</param>
        /// <param name="value">The value to create.</param>
        /// <param name="expirationTime">The expiration time for the cache entry. If specified, overrides the default `cacheOptions`.</param>
        /// <returns>The created value.</returns>
        public object CreateValue(object key, object value, double? expirationTime = null)
        {
            //verify if special entryOption was set
            if (expirationTime != null) cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(expirationTime.Value)
            };

            //verify if value exists first to avoid exceptions
            object? internalValue = GetValueInternal<object>(key);

            //if value exists then delete it
            if (internalValue != null) _cache.Remove(key);

            //create the value again
            _cache.Set(key, value, cacheOptions);


            return value;
        }
         
        public T CreateValue<T>(object key, T value, double? expirationTime = null)
        {
            //verify if special entryOption was set
            if (expirationTime != null) cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(expirationTime.Value)
            };

            //verify if value exists first to avoid exceptions
            T? internalValue = GetValueInternal<T>(key);

            //if value exists then delete it
            if (internalValue != null) _cache.Remove(key);

            //create the value again
            _cache.Set<T>(key, value, cacheOptions);


            return value;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key, or default(object) if the key is not found.</returns>
        public object? GetValue(object key)
        {
            return GetValue<object>(key);
        }


        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the value to get.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key, or default(T) if the key is not found.</returns>
        public T GetValue<T>(object key)
        {
            return GetValueInternal<T>(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key, meant to be used only inside this class.
        /// </summary>
        /// <typeparam name="T">The type of the value to get.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key, or default(T) if the key is not found.</returns>
        private T GetValueInternal<T>(object key)
        {
            if (key == null) return default(T);

            T internalValue;

            // Attempt to get the value from the cache
            _cache.TryGetValue<T>(key, out internalValue);

            return internalValue;
        }


    }
}
