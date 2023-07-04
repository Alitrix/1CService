using _1CService.Application.DTO.Request;

namespace _1CService.Application.UseCases.Profile
{
    public interface ISetProfileAppUser
    {
        Task<bool> Set(RequestSetAppUserProfileDTO request);
    }
}