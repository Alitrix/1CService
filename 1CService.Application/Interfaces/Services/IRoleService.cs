using _1CService.Application.Enums;
using _1CService.Application.Models;

namespace _1CService.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<bool> AddRoleToUser(AppUser user, string userType);
        Task<Guid> GenerateGuidFromRole(string userTypeAccess, AppUser? appUser = null);
        string GetRoleByGuid(AppUser user, string guid);
    }
}