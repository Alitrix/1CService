using _1CService.Application.Interfaces.Repositories;
using System.Collections.Concurrent;

namespace _1CService.Persistence.Repository
{
    public class LocalDatabaseGuidRole : ILocalDatabaseGuidRole
    {
        private ConcurrentDictionary<string, Guid> KeyPairTokenGuid;
        public LocalDatabaseGuidRole()
        {
            KeyPairTokenGuid = new();
        }
        public void Add(string key, Guid value)
        {
            if (KeyPairTokenGuid.ContainsKey(key))
                return;

            var kp = KeyPairTokenGuid.GetOrAdd(key, value);
        }

        public Guid GetGuid(string key) 
        {
            if (!KeyPairTokenGuid.ContainsKey(key))
                return Guid.Empty;
            
            Guid guid = Guid.Empty;
            KeyPairTokenGuid.Remove(key, out guid);
            return guid;
        }
    }
}
