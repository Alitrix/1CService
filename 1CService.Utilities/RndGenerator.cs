using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _1CService.Utilities
{
    public static class RndGenerator
    {
        public static RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        public static Func<string> GenerateSecurityStamp = delegate ()
        {
            byte[] array = ArrayPool<byte>.Shared.Rent(32);
            Span<byte> bytes = new Span<byte>(array);
            
            randomNumberGenerator.GetNonZeroBytes(bytes);
            return string.Concat(Array.ConvertAll(array, b => b.ToString("X2")));
        };
    }
}
