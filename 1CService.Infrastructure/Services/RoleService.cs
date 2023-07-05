using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        public static Dictionary<string, Tuple<string, Guid>> GuidRole = new();//Need to request Redis server

        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(IAppUserService appUserService,
                            UserManager<AppUser> userManager,
                            RoleManager<IdentityRole> roleManager) =>
            (_appUserService, _userManager, _roleManager) = (appUserService, userManager, roleManager);

        public async Task<bool> Add(AppUser user, string userType)
        {
            if (user == null) return false;
            
            if (!await _roleManager.RoleExistsAsync(userType)) return false;

            if (await _userManager.IsInRoleAsync(user, userType)) return false;

            var identityResult = await _userManager.AddToRoleAsync(user, userType);
            if (!identityResult.Succeeded) return false;

            return true;
        }
        public async Task<bool> InRole(string role, AppUser? appUser = null)
        {
            var user = appUser ?? await _appUserService.GetCurrentUser();
            return await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<Guid> GenericGuidToRole(string userTypeAccess, AppUser? appUser = null)
        {
            var user = appUser ?? await _appUserService.GetCurrentUser();
            if(user == null) return Guid.Empty;

            if(await InRole(userTypeAccess, user))
                return Guid.Empty;

            var guid = Guid.NewGuid();

            if (GuidRole.ContainsKey(user.Id))
                return Guid.Empty;

            GuidRole.Add(user.Id, new Tuple<string, Guid>(userTypeAccess, guid));
            return guid;
        }
        public string GetRoleByGuid(AppUser user, string guid)
        {
            var fndItem = GuidRole.SingleOrDefault(x => x.Key == user.Id);
            if (fndItem.Value == null)
                return string.Empty;

            var tapGuidRole = fndItem.Value;
            if (tapGuidRole == null)
                return string.Empty;
            if (tapGuidRole.Item2 != new Guid(guid))
                return string.Empty;

            GuidRole.Remove(fndItem.Key);
            return tapGuidRole.Item1.ToString();
        }
    }
}
