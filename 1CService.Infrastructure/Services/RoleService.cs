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

        public async Task<bool> AddRoleToUser(AppUser user, string role)
        {
            if (user == null) return false;
            
            if (!await _roleManager.RoleExistsAsync(role).ConfigureAwait(false)) return false;

            if (await _userManager.IsInRoleAsync(user, role).ConfigureAwait(false)) return false;

            var identityResult = await _userManager.AddToRoleAsync(user, role).ConfigureAwait(false);
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
        public async Task<string> GetRoleByGuid(string token_guid)
        {
            UserRoleRequestItem? userItemRequestGuidRole = await _redisService.Get<UserRoleRequestItem>(token_guid).ConfigureAwait(false);
            
            if (userItemRequestGuidRole == null) return string.Empty;

            return await _redisService.Remove(token_guid).ConfigureAwait(false)? 
                userItemRequestGuidRole.Role: string.Empty;
        }
    }
}
