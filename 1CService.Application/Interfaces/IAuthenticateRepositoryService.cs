using _1CService.Domain.Models;

namespace _1CService.Application.Interfaces
{
    public interface IAuthenticateRepositoryService
    {
        Task<AppUser> GetCurrentUser();
    }
}