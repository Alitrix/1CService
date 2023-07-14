using _1CService.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace _1CService.Infrastructure.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache) =>
                _distributedCache = distributedCache;

        public async Task<T?> Get<T>(string id)
        {
            string? retValue = await _distributedCache.GetStringAsync(id).ConfigureAwait(false);
            if (retValue != null)
            {
                try
                {
                    T? data = JsonSerializer.Deserialize<T>(retValue);
                    if (data != null) return data;
                    else
                        return default;
                }
                catch
                {
                    return default;
                }
            }
            return default;
        }
        public bool Set<T>(string id, T value, TimeSpan time)
        {
            try
            {
                if (value is null) return false;

                var jsonValue = JsonSerializer.Serialize(value);
                _distributedCache.SetString(id, jsonValue, new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = time
                });
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> ContainsKey(string id)
        {
            return string.IsNullOrEmpty(await _distributedCache.GetStringAsync(id).ConfigureAwait(false))? false : true;
        }
        public async Task<bool> Remove(string id)
        {
            if(string.IsNullOrEmpty(id)) return false;

            if(!await ContainsKey(id)) return false;

            await _distributedCache.RemoveAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}
