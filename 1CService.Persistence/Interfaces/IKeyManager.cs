using System.Security.Cryptography;

namespace _1CService.Persistence.Interfaces
{
    public interface IKeyManager
    {
        RSA RsaKey { get; }
    }
}