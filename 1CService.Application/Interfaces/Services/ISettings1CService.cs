using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Services
{
    public interface ISettings1CService
    {
        Task<Settings> GetHTTPService1CSettings();
    }
}
