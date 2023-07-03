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
        public const string ISSUER = "SMYK.1CService"; // издатель
        public const string AUDIENCE = "SMYK.1CService.Mobile"; // потребитель
        const string KEY = "C1CF4B7DC4C4175B6618DNHY&U*(IJNBGY&U*(OIKJNHBYU&*()OKMJNBHYU&*(I)OKMJNHBYU*(IOKIJNBHGYU&*(IKIJNGVTY&U*HBVCFDES#WSEDXCFVGBHNJMKOL)P__)PL< BHGY^T&*(IIUHGTFR%T^&*(EDFGBHJ%^&U*IOFVBGNMJK^&U*I(O)VFBNHJMKL^&*(O4F55CA4";
        public const int LIFETIME = 15;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
