using _1CService.Application.DTO;
using _1CService.Application.Enums;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Infrastructure.Services
{
    public class EmailTokenService : IEmailTokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly IRedisService _redisService;
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsPrincipalFactory;

        public EmailTokenService(UserManager<AppUser> userManager, IAppUserService appUserService, 
            IRedisService redisService, IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory) =>
            (_userManager, _appUserService, _redisService, _claimsPrincipalFactory) = 
            (userManager,appUserService, redisService, claimsPrincipalFactory);

        public async Task<string> GenerateEmailConfirmationToken(AppUser? user = null)
        {
            var currentUser = user ?? await _appUserService.GetCurrentUser().ConfigureAwait(false);
            return currentUser == null ? string.Empty : await _userManager.GenerateEmailConfirmationTokenAsync(currentUser).ConfigureAwait(false);
        }
        public async Task<bool> ValidationEmailToken(string userid, string token)
        {
            var registrationAppUSerRedis = await _redisService.Get<PreRegistrationAppUserDTO>(userid).ConfigureAwait(false);
            if (registrationAppUSerRedis == null)
                return false;

            IdentityResult createUserResult = await _userManager.CreateAsync(registrationAppUSerRedis.User).ConfigureAwait(false);
            if (createUserResult.Succeeded)
            {
                AppUser user = registrationAppUSerRedis.User;

                if (!await _redisService.Remove(userid).ConfigureAwait(false)) return false;
                    
                user.PasswordHash = registrationAppUSerRedis.Password;
                IdentityResult updateRet = await _userManager.UpdateAsync(user);
                if (!updateRet.Succeeded) return false;

                var principal = await _claimsPrincipalFactory.CreateAsync(user).ConfigureAwait(false);
                var identity = principal.Identities.First();

                createUserResult = await _userManager.AddToRoleAsync(user, UserTypeAccess.User).ConfigureAwait(false);
                if (createUserResult.Succeeded)
                {
                    createUserResult = await _userManager.AddClaimsAsync(user, identity.Claims).ConfigureAwait(false);
                    if (!createUserResult.Succeeded)
                        return false;

                    var retLockout = await _userManager.SetLockoutEnabledAsync(user, false).ConfigureAwait(false);
                    if (!retLockout.Succeeded)
                        return false;
                    
                    if (string.Equals(registrationAppUSerRedis.EmailTokenConfirm, token))
                    {
                        user.EmailConfirmed = true;
                        await _userManager.UpdateAsync(user);
                        await _userManager.SetLockoutEnabledAsync(user, true).ConfigureAwait(false);
                        return true;
                    }
                    await _userManager.SetLockoutEnabledAsync(user, true).ConfigureAwait(false);
                    /*var checkedConfirm = await _userManager.ConfirmEmailAsync(user, token).ConfigureAwait(false);
                    if (!checkedConfirm.Succeeded)
                        return false;
                    return true;*/
                }
            }
            return false;
        }
    }
}
