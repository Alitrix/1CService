using _1CService.Application.Models.Profile.Request;

namespace _1CService.Application.Interfaces.Services
{
    public interface IProfileService
    {
        Task<bool> Save(SetAppUserProfileQuery request);
    }
}