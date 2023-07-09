using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Profile.Response;

namespace _1CService.Application.UseCases.ProfileHandler.Queries
{
    public class GetProfile : IGetProfileAppUser
    {
        private readonly IAppUserService _appUserService;

        public GetProfile(IAppUserService appUserService) => _appUserService = appUserService;
        public async Task<AppUserProfile> Get()
        {
            var usrSettings = await _appUserService.GetAppUserProfile().ConfigureAwait(false);

            return new AppUserProfile()
            {
                User1C = usrSettings.User1C,
                Password1C = usrSettings.Password1C,
            };
        }
    }
}
