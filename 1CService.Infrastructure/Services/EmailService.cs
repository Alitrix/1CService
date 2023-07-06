using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;

        public EmailService(IAppUserService appUserService, UserManager<AppUser> userManager)
        {
            _appUserService = appUserService;
            _userManager = userManager;
        }

        public async Task<string> GenerateEmailConfirmationToken(AppUser? user = null)
        {
            var currentUser = user?? await _appUserService.GetCurrentUser();
            if (currentUser == null)
                return string.Empty;

            var originalCode = await _userManager.GenerateEmailConfirmationTokenAsync(currentUser);
            return originalCode;
        }
    }
}
