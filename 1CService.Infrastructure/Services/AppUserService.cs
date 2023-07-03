using _1CService.Application.DTO;
using _1CService.Application.Enums;
using _1CService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace _1CService.Infrastructure.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IHttpContextAccessor _ctxa;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IHttpContextAccessor ctxa,
                                SignInManager<AppUser> signInManager)
        {
            _ctxa = ctxa;
            _signInManager = signInManager;
        }
        public async Task<AppUser?> GetCurrentUser()
        {
            var user = await _signInManager.UserManager.FindByNameAsync(_ctxa.HttpContext.User.FindFirstValue(ClaimTypes.Name));
            return await Task.FromResult(user);
        }
        public async Task<IList<Claim>> GetCurrentClaims()
        {
            var currentUser = await GetCurrentUser().ConfigureAwait(false);
            if (currentUser == null)
                return new List<Claim>();

            var claims = await _signInManager.UserManager.GetClaimsAsync(currentUser);
            var roles = await _signInManager.UserManager.GetRolesAsync(currentUser);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }
        public async Task<List<Claim>> GetClaimsAndRoles(AppUser? appUser = null)
        {
            var user = appUser?? await GetCurrentUser().ConfigureAwait(false);

            var claims = await _signInManager.UserManager.GetClaimsAsync(user);
            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims.ToList();
        }
        public bool? IsAuthenticate()
        {
            if (_ctxa == null ||
                _ctxa.HttpContext == null ||
                _ctxa.HttpContext.User == null ||
                _ctxa.HttpContext.User.Identity == null)
                return false;

            return _ctxa?.HttpContext?.User?.Identity?.IsAuthenticated;
        }
    }
}
