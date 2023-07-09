using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace _1CService.Infrastructure.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IHttpContextAccessor _ctxa;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IHttpContextAccessor ctxa, SignInManager<AppUser> signInManager) =>
            (_ctxa, _signInManager) = (ctxa, signInManager);


        public async Task<AppUser?> GetCurrentUser()
        {
            var user = _ctxa?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            if(user == null)
                return null;
            
            return await _signInManager.UserManager.FindByNameAsync(user).ConfigureAwait(false);
        }
        public async Task<AppUser?> GetUserById(string user_id)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(user_id).ConfigureAwait(false);
            if (user == null)
                return null;
            return user;
        }
        public async Task<IList<Claim>> GetCurrentClaims()
        {
            var currentUser = await GetCurrentUser().ConfigureAwait(false);
            if (currentUser == null)
                return new List<Claim>();

            var claims = await _signInManager.UserManager.GetClaimsAsync(currentUser).ConfigureAwait(false);
            var roles = await _signInManager.UserManager.GetRolesAsync(currentUser).ConfigureAwait(false);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }
        public async Task<List<Claim>> GetClaimsAndRoles(AppUser? appUser = null)
        {
            var user = appUser?? await GetCurrentUser().ConfigureAwait(false);
            if(user == null )
                return new List<Claim>();

            var claims = await _signInManager.UserManager.GetClaimsAsync(user).ConfigureAwait(false);
            var roles = await _signInManager.UserManager.GetRolesAsync(user).ConfigureAwait(false);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims.ToList();
        }
        public async Task<AppUser1CProfileDTO> GetAppUserProfile()
        {
            var currentUser = await GetCurrentUser().ConfigureAwait(false);
            if(currentUser == null)
                return default;

            AppUser1CProfileDTO profile = new ()
            {
                User1C = currentUser.User1C,
                Password1C = currentUser.Password1C,
            };
            return profile;
        }
        public async Task<ServiceProfileDTO> GetServiceProfile()
        {
            var currentUser = await GetCurrentUser().ConfigureAwait(false);
            if (currentUser == null)
                return await Task.FromResult(default(ServiceProfileDTO));

            ServiceProfileDTO settings = new ()
            {
                ServiceAddress = currentUser.ServiceAddress,
                ServiceSection = currentUser.ServiceSection,
                ServiceBaseName = currentUser.ServiceBaseName
            };
            return settings;

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
