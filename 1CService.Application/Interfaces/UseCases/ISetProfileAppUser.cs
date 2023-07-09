using _1CService.Application.Models.Profile.Request;

namespace _1CService.Application.Interfaces.UseCases
{
    public interface ISetProfileAppUser
    {
        Task<bool> Set(SetAppUserProfileQuery request);
    }
}