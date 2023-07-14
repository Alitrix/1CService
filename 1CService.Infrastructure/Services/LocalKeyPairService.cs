using _1CService.Application.Interfaces.Services;
using System.Collections.Concurrent;

namespace _1CService.Infrastructure.Services
{
    public class LocalKeyPairService : IRedisService
    {
        private readonly ConcurrentDictionary<string, object> keyValuePairs; 
        public LocalKeyPairService() => keyValuePairs = new();

        public async Task<bool> ContainsKey(string id)
        {
            return await Task.FromResult(keyValuePairs.ContainsKey(id)).ConfigureAwait(false);
        }

        public async Task<T?> Get<T>(string id)
        {
            if(keyValuePairs.ContainsKey(id))
                return await Task.FromResult((T)keyValuePairs[id]).ConfigureAwait(false);
            else
                return default;
        }

        public async Task<bool> Remove(string id)
        {
            return await Task.FromResult( keyValuePairs.Remove(id, out var _)).ConfigureAwait(false);
        }

        public bool Set<T>(string id, T value, TimeSpan time)
        {
            return keyValuePairs.GetOrAdd(id, value) != null;
        }
    }
}
