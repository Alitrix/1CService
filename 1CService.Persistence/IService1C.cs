using _1CService.Application.DTO;

namespace _1CService.Persistence
{
    public interface IService1C
    {
        Task<T> GetAsync<T>(HttpClient client, string nameFunc);
        Task<T> PostAsync<T>(HttpClient client, string nameFunc, HttpContent param);
        HttpClient InitJsonContext();
        HttpClient InitTextContext();
        void SetSettings(Settings profile);
    }
}
