using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Profile.Response;

namespace _1CService.Application.UseCases.ProfileHandler.Queries
{
    public class GetProfileAppUser : IGetProfileAppUser
    {
        private readonly IAppUserService _appUserService;

        public GetProfileAppUser(IAppUserService appUserService) => _appUserService = appUserService;
        public async Task<AppUserProfile> Get()
        {
            var usrSettings = await _appUserService.GetAppUserProfile();

            return new AppUserProfile()
            {
                User1C = usrSettings.User1C,
                Password1C = usrSettings.Password1C,
            };
        }
    }
}
