using _1CService.Application.DTO;

namespace _1CService.Persistence.API
{
    public interface IService1C
    {
        Task<T> GetAsync<T>(HttpClient client, string nameFunc);
        Task<T> PostAsync<T>(HttpClient client, string nameFunc, HttpContent param);
        Task<HttpClient> InitJsonContext();
        Task<HttpClient> InitTextContext();
        void SetSettings(Settings profile);
    }
}
