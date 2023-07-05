using _1CService.Application.Models.Profile.Request;

namespace _1CService.Application.UseCases.ProfileHandler.Command
{
    public interface ISetProfileAppUser
    {
        Task<bool> Set(SetAppUserProfileQuery request);
    }
}