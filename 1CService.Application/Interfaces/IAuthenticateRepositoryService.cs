using _1CService.Application.DTO;
using _1CService.Domain.Models;

namespace _1CService.Application.Interfaces
{
    public interface IAuthenticateRepositoryService
    {
        Task<AppUser> GetCurrentUser();
        Task<Settings> GetUserProfile(AppUser user);
    }
}