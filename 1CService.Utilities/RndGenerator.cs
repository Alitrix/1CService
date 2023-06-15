using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Utilities
{
    public static class RndGenerator
    {
        public static RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        public static Func<string> GenerateSecurityStamp = delegate ()
        {
            byte[] rand = new byte[32];
            
            randomNumberGenerator.GetNonZeroBytes(rand);
            
            return string.Concat(Array.ConvertAll(rand, b => b.ToString("X2")));
        };
    }
}
