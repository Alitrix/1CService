using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using _1CService.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Persistence.Services
{
    public class AuthenticationService : IAuthenticateService
    {
        private readonly IAppUserDbContext _context;

        public AuthenticationService(IAppUserDbContext context)
        {
            _context = context;
        }

        public Task<AppUser> GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> TryLogin(AuthDTO authDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> TrySignIn(SignInDTO signInDTO)
        {
            throw new NotImplementedException();
        }
    }
}
