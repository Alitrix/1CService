using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticateService
    {
        private readonly IAppUserDbContext _context;
        private readonly IHttpContextAccessor _ctxa;

        public AuthenticationService(IAppUserDbContext context, IHttpContextAccessor ctxa, 
                SignInManager<AppUser> signInManager,
                UserManager<AppUser> userManager,
                IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory)
        {
            _context = context;
            _ctxa = ctxa;
        }

        public Task<AppUser?> GetCurrentUser()
        {
            var claimPrincipalEmail = _ctxa.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            AppUser? user = _context.Users?.FirstOrDefault(x => x.Email == claimPrincipalEmail);
            return Task.FromResult(user);
        }
        public Task<IdentityResult> SignUp(SignUpDTO signUpDTO) //Registering Account
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> SignIn(SignInDTO signInDTO) //Autorization Account
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> SignOut(AppUser user) // Exit Account
        {
            throw new NotImplementedException();
        }

    }
}
