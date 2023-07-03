using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Persistence.Services
{
    public class Settings1CService : ISettings1CService
    {
        private readonly IAppUserService _appUserService;

        public Settings1CService(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        public async Task<Settings> GetHTTPService1CSettings()
        {
            var currentUser = await _appUserService.GetCurrentUser();
            if (currentUser == null)
                return await Task.FromResult(default(Settings));

            Settings settings = new Settings();
            settings.User1C = currentUser.User1C;
            settings.Password1C = currentUser.Password1C;
            settings.ServiceAddress = currentUser.ServiceAddress;
            settings.ServiceSection = currentUser.ServiceSection;
            settings.ServiceBaseName = currentUser.ServiceBaseName;
            return await Task.FromResult(settings);

        }
    }
}
