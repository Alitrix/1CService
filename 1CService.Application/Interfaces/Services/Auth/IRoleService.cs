using _1CService.Application.DTO;
using _1CService.Application.Enums;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface IRoleService
    {
        Task<bool> Add(AppUser user, string userType);
        Task<Guid> GenericGuidToRole(string userTypeAccess, AppUser? appUser = null);
        string GetRoleByGuid(AppUser user, string guid);
    }
}