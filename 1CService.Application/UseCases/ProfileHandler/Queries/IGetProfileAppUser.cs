using _1CService.Application.Models.Profile.Response;

namespace _1CService.Application.UseCases.ProfileHandler.Queries
{
    public interface IGetProfileAppUser
    {
        Task<AppUserProfile> Get();
    }
}