using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class EmailConfirmUser : IEmailConfirmUser
    {
        private readonly UserManager<AppUser> _userManager;

        public EmailConfirmUser(UserManager<AppUser> userManager) =>
            _userManager = userManager ;

        public async Task<bool> EmailTokenValidation(string userid, string token)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
                return false;

            var retLockout = await _userManager.SetLockoutEnabledAsync(user, false);
            if(!retLockout.Succeeded)
                return false;

            var checkedConfirm = await _userManager.ConfirmEmailAsync(user, token);
            if (!checkedConfirm.Succeeded)
                return false;

            return true;
        }
    }
}
