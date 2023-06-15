using _1CService.Application.DTO;
using _1CService.Persistence.Enums;

namespace _1CService.Persistence.Interfaces
{
    public interface IService1C
    {
        Task<T> GetAsync<T>(HttpClient client, string nameFunc);
        Task<T> PostAsync<T>(HttpClient client, string nameFunc, HttpContent param);
        Task<HttpClient> InitContext(TypeContext1CService serviceType);
    }
}
