using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class RoleAddToUser : IRoleAddToUser
    {
        private readonly IAppUserService _appUserService;
        private readonly IRoleService _roleService;
        private readonly ILocalDatabaseGuidRole _localDatabaseGuidRole;

        public RoleAddToUser(IAppUserService appUserService,
                                IRoleService roleService, 
                                ILocalDatabaseGuidRole localDatabaseGuidRole) =>
            (_appUserService, _roleService, _localDatabaseGuidRole) = (appUserService, roleService, localDatabaseGuidRole);

        public async Task<AddRoleResponse> AddRole(string user_id, string token_guid)
        {
            var guid = _localDatabaseGuidRole.GetGuid(token_guid);
            if(Guid.Empty == guid)
                return default;

            var user = await _appUserService.GetUserById(user_id);
            if(user == null) return default;

            var roleFromGuid = _roleService.GetRoleByGuid(user, guid.ToString());
            if(roleFromGuid == null)
                return default;

            var retAdd = await _roleService.AddRoleToUser(user, roleFromGuid);
            return new AddRoleResponse() { Error = "", Success = retAdd };

        }
    }
}
