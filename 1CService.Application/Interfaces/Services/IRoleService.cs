using _1CService.Application.DTO;
using _1CService.Application.Enums;
using _1CService.Application.Models;

namespace _1CService.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<bool> AddRoleToUser(AppUser user, string userType);
        Task<UserRoleRequestItem?> GenerateGuidFromRoleForUser(string userTypeAccess, AppUser user);
        Task<string> GetRoleByGuid(AppUser user, string guid);
    }
}