using _1CService.Application.DTO.Response;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface IRoleAddToUser
    {
        Task<AddRoleResponse> AddRole(string guid);
    }
}