using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Profile.Request;

namespace _1CService.Application.UseCases.ProfileHandler.Command
{
    public class SetProfile : ISetProfileAppUser
    {
        private readonly IProfileService _profileService;

        public SetProfile(IProfileService profileService) => 
            _profileService = profileService;

        public async Task<bool> Set(SetAppUserProfileQuery request)
        {
            return await _profileService.Save(request);
        }
    }
}
