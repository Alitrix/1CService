using System.Security.Cryptography;

namespace _1CService.Utilities.Interfaces
{
    public interface IKeyManager
    {
        RSA RsaKey { get; }
    }
}