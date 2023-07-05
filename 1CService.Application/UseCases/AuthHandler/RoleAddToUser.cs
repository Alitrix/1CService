using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.UseCases.Auth
{
    public class RoleAddToUser : IRoleAddToUser
    {
        private readonly IAppUserService _userService;
        private readonly IRoleService _roleService;

        public RoleAddToUser(IAppUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<AddRoleResponse> AddRole(string guid)
        {
            var currentUser = await _userService.GetCurrentUser();
            var roleFromGuid = _roleService.GetRoleByGuid(currentUser, guid);
            var retAdd = await _roleService.Add(currentUser, roleFromGuid);

            return new AddRoleResponse() { Error = "", Success = retAdd };

        }
    }
}
