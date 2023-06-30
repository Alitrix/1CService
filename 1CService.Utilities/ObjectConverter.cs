using System.Text;
using System.Text.Json;

namespace _1CService.Utilities
{
    public static class ObjectConverter
    {
        public static T? ToObj<T>(this string jsonStr) => JsonSerializer.Deserialize<T>(jsonStr);
        public static T? ToObj<T>(this byte[] data) => JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(data));
        public static string ToJsonString(this object obj) => JsonSerializer.Serialize(obj);
    }
}