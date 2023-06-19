using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using _1CService.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace _1CService.Persistence.Services
{
    public class AuthenticationService : IAuthenticateService
    {
        private readonly IAppUserDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IAppUserDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<AppUser> GetCurrentUser()
        {
            var claimPrincipalEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            AppUser user = _context.Users.FirstOrDefault(x => x.Email == claimPrincipalEmail);
            return Task.FromResult(user);
        }

        public Task<IdentityResult> TryLogin(AuthDTO authDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> TryLogOut(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> TrySignIn(SignInDTO signInDTO)
        {
            throw new NotImplementedException();
        }
    }
}
