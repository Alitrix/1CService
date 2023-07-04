using _1CService.Application.DTO.Response;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Application.UseCases.Profile
{
    public class GetProfileAppUser : IGetProfileAppUser
    {
        private readonly IAppUserService _appUserService;

        public GetProfileAppUser(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        public async Task<ResponseGetProfile> Get()
        {
            var usrSettings = await _appUserService.GetAppUserProfile();

            return new ResponseGetProfile()
            {
                User1C = usrSettings.User1C,
                Password1C = usrSettings.Password1C,
            };
        }
    }
}
