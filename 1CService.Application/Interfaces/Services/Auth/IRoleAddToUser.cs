using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface IRoleAddToUser
    {
        Task<AddRoleResponse> AddRole(string guid);
    }
}