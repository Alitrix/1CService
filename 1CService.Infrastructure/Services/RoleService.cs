using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRedisService _redisService;

        public RoleService(IAppUserService appUserService,
                            UserManager<AppUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            IRedisService redisService) =>
            (_appUserService, _userManager, _roleManager, _redisService) = (appUserService, userManager, roleManager, redisService);

        public async Task<bool> AddRoleToUser(AppUser user, string userType)
        {
            if (user == null) return false;
            
            if (!await _roleManager.RoleExistsAsync(userType).ConfigureAwait(false)) return false;

            if (await _userManager.IsInRoleAsync(user, userType).ConfigureAwait(false)) return false;

            var identityResult = await _userManager.AddToRoleAsync(user, userType).ConfigureAwait(false);
            if (!identityResult.Succeeded) return false;

            return true;
        }
        public async Task<bool> InRole(string role, AppUser? appUser = null)
        {
            var user = appUser ?? await _appUserService.GetCurrentUser().ConfigureAwait(false);
            if (user == null) return false;
            return await _userManager.IsInRoleAsync(user, role).ConfigureAwait(false);
        }
        public async Task<UserRoleRequestItem?> GenerateGuidFromRoleForUser(string userTypeAccess, AppUser user)
        {
            if(user == null) return null;

            if(await InRole(userTypeAccess, user).ConfigureAwait(false))
                return null;

            if(await _redisService.ContainsKey(user.Id).ConfigureAwait(false))
                return null;
            
            var item = new UserRoleRequestItem() { User = user, Role = userTypeAccess, TokenGuid = Guid.NewGuid(), };
            return item;
        }
        public async Task<string> GetRoleByGuid(AppUser user, string guid)
        {
            UserRoleRequestItem? userItemRequestGuidRole = await _redisService.Get<UserRoleRequestItem>(user.Id).ConfigureAwait(false);
            
            if (userItemRequestGuidRole == null) return string.Empty;

            if (userItemRequestGuidRole.TokenGuid != new Guid(guid)) return string.Empty;

            return await _redisService.Remove(userItemRequestGuidRole.User.Id).ConfigureAwait(false)? 
                userItemRequestGuidRole.Role: string.Empty;
        }
    }
}
