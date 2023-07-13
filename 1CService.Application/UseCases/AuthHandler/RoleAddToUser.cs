using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class RoleAddToUser : IRoleAddToUser
    {
        private readonly IAppUserService _appUserService;
        private readonly IRoleService _roleService;

        public RoleAddToUser(IAppUserService appUserService, IRoleService roleService, IRedisService redisService) =>
            (_appUserService, _roleService) = (appUserService, roleService);

        public async Task<AddRoleResponse> AddRole(string user_id, string token_guid)
        {
            var user = await _appUserService.GetUserById(user_id);
            if(user == null) return default;

            var roleFromGuid = await _roleService.GetRoleByGuid(token_guid).ConfigureAwait(false);
            if(roleFromGuid == null)
                return default;

            var retAdd = await _roleService.AddRoleToUser(user, roleFromGuid);
            return new AddRoleResponse() { Error = "", Success = retAdd };

        }
    }
}
