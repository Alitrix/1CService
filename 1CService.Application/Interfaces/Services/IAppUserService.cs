using _1CService.Application.DTO;
using _1CService.Application.Models;
using System.Security.Claims;

namespace _1CService.Application.Interfaces.Services
{
    public interface IAppUserService
    {
        Task<IList<Claim>> GetCurrentClaims();
        Task<AppUser?> GetCurrentUser();
        Task<AppUser?> GetUserById(string user_id);
        bool? IsAuthenticate();
        Task<List<Claim>> GetClaimsAndRoles(AppUser? user = null);
        Task<ServiceProfileDTO> GetServiceProfile();
        Task<AppUser1CProfileDTO> GetAppUserProfile();
    }
}