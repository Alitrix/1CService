using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using _1CService.Domain.Models;
using _1CService.Persistence.Interfaces;

namespace _1CService.Persistence.Services
{
    public class Settings1CService : ISettings1CService
    {
        private readonly IAuthenticateService _authenticateService;

        public Settings1CService(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        public async Task<Settings> GetHttpServiceSettings()
        {
            AppUser currentUser = await _authenticateService.GetCurrentUser();

            Settings settings = new Settings();
            settings.User1C = currentUser.User1C;
            settings.Password1C = currentUser.Password1C;

            settings.ServiceAddress = "srv";
            settings.ServiceSection = "MobileService";
            settings.ServiceBaseName = "smyk2";

            return await Task.FromResult(settings);
        }
    }
}
