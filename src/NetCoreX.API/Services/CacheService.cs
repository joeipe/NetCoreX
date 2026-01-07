using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using System.Text.Json;

namespace NetCoreX.API.Services
{
    public class CacheService
    {
        private readonly HybridCache _cache;

        public CacheService(HybridCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetFromCacheAsync<T>(string key, Func<Task<T>> handleAsync) where T : class
        {
            var response = await _cache.GetOrCreateAsync(key, async entry => await handleAsync());
            return response;
        }

        public async Task ClearCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
