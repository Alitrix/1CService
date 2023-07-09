using _1CService.Application.Models.Profile.Response;

namespace _1CService.Application.Interfaces.UseCases
{
    public interface IGetProfileAppUser
    {
        Task<AppUserProfile> Get();
    }
}