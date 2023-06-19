using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using _1CService.Utilities.Interfaces;

namespace _1CService.Utilities
{
    public class KeyManager : IKeyManager
    {
        public RSA RsaKey { get; }
        public KeyManager()
        {
            RsaKey = RSA.Create();
            if (File.Exists("key"))
            {
                RsaKey.ImportRSAPrivateKey(File.ReadAllBytes("key"), out _);
            }
            else
            {
                var privateKey = RsaKey.ExportRSAPrivateKey();
                File.WriteAllBytes("key", privateKey);
            }
        }
    }
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "C1CF4B7DC4C4175B6618DNHY&U*(IJNBGY&U*(OIKJNHBYU&*()OKMJNBHYU&*(I)OKMJNHBYU*(IOKIJNBHGYU&*(IKIJNGVTY&U*HBVCFDES#WSEDXCFVGBHNJMKOL)P__)PL< BHGY^T&*(IIUHGTFR%T^&*(EDFGBHJ%^&U*IOFVBGNMJK^&U*I(O)VFBNHJMKL^&*(O4F55CA4";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
