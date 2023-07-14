namespace _1CService.Application.Interfaces.Services
{
    public interface IRedisService
    {
        Task<T?> Get<T>(string id);
        bool Set<T>(string id, T value, TimeSpan time);
        Task<bool> ContainsKey(string id);
        Task<bool> Remove(string id);
    }
}