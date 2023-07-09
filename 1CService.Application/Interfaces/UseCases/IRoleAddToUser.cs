using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.Interfaces.UseCases
{
    public interface IRoleAddToUser
    {
        Task<AddRoleResponse> AddRole(string user_id, string token_guid);
    }
}