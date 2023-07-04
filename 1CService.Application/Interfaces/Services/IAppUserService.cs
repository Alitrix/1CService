using _1CService.Application.DTO;
using System.Security.Claims;

namespace _1CService.Application.Interfaces.Services
{
    public interface IAppUserService
    {
        Task<IList<Claim>> GetCurrentClaims();
        Task<AppUser?> GetCurrentUser();
        bool? IsAuthenticate();
        Task<List<Claim>> GetClaimsAndRoles(AppUser? user = null);
        Task<ServiceProfile> GetServiceProfile();
        Task<AppUser1CProfile> GetAppUserProfile();
    }
}