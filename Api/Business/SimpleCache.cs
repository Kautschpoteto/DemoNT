using Microsoft.Extensions.Caching.Memory;
using System;

namespace Api.Business
{
    public class SimpleCache<T> : ISimpleCache<T>
    {
        #region Private Fields

        private readonly IMemoryCache _cache;

        #endregion Private Fields

        #region Public Constructors

        public SimpleCache(IMemoryCache memoryCache)
        {
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        #endregion Public Constructors

        #region Public Methods

        public T Get(string city)
        {
            T data;

            return _cache.TryGetValue(city, out data) ? data: default ;
        }

        public void Set(string key, T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = (DateTime.Now.AddMinutes(2) - DateTime.Now)
            };

            _cache.Set(key, data, cacheEntryOptions);
        }

        #endregion Public Methods
    }
}
