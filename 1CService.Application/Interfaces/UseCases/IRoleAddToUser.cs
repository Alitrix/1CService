using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.Interfaces.UseCases
{
    public interface IRoleAddToUser
    {
        Task<AddRoleResponse> AddRole(string guid);
    }
}