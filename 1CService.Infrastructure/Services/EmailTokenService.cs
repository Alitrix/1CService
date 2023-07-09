using _1CService.Application.Interfaces.Services;
using _1CService.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Infrastructure.Services
{
    public class EmailTokenService : IEmailTokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;

        public EmailTokenService(UserManager<AppUser> userManager, IAppUserService appUserService) =>
            (_userManager, _appUserService) = (userManager,appUserService);

        public async Task<string> GenerateEmailConfirmationToken(AppUser? user = null)
        {
            var currentUser = user ?? await _appUserService.GetCurrentUser().ConfigureAwait(false);
            return currentUser == null ? string.Empty : await _userManager.GenerateEmailConfirmationTokenAsync(currentUser).ConfigureAwait(false);
        }
        public async Task<bool> ValidationEmailToken(string userid, string token)
        {
            var user = await _userManager.FindByIdAsync(userid).ConfigureAwait(false);
            if (user == null)
                return false;

            var retLockout = await _userManager.SetLockoutEnabledAsync(user, false).ConfigureAwait(false);
            if (!retLockout.Succeeded)
                return false;

            var checkedConfirm = await _userManager.ConfirmEmailAsync(user, token).ConfigureAwait(false);
            if (!checkedConfirm.Succeeded)
                return false;

            return true;
        }
    }
}
