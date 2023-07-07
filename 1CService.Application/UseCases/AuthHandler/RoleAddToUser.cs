using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class RoleAddToUser : IRoleAddToUser
    {
        private readonly IAppUserService _userService;
        private readonly IRoleService _roleService;

        public RoleAddToUser(IAppUserService userService, IRoleService roleService) =>
            (_userService, _roleService) = (userService, roleService);

        public async Task<AddRoleResponse> AddRole(string guid)
        {
            var currentUser = await _userService.GetCurrentUser();
            if (currentUser == null)
                return default;

            var roleFromGuid = _roleService.GetRoleByGuid(currentUser, guid);
            if(roleFromGuid == null)
                return default;

            var retAdd = await _roleService.Add(currentUser, roleFromGuid);
            return new AddRoleResponse() { Error = "", Success = retAdd };

        }
    }
}
