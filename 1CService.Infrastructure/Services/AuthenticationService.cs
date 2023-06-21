using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Interfaces.Services;

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

        public Task<AppUser?> GetCurrentUser()
        {
            var claimPrincipalEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            AppUser? user = _context.Users?.FirstOrDefault(x => x.Email == claimPrincipalEmail);
            return Task.FromResult(user);
        }

        public Task<IdentityResult> SignIn(AuthDTO authDTO) //Autorization Account
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> SignOut(AppUser user) // Exit Account
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> SignUp(SignInDTO signInDTO) //Registering Account
        {
            throw new NotImplementedException();
        }
    }
}
