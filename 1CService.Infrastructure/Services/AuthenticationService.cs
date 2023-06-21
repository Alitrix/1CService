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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsPrincipalFactory;

        public AuthenticationService(IAppUserDbContext context, IHttpContextAccessor ctxa, 
                SignInManager<AppUser> signInManager,
                UserManager<AppUser> userManager,
                IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory)
        {
            _context = context;
            _ctxa = ctxa;
            _signInManager = signInManager;
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        public Task<AppUser?> GetCurrentUser()
        {
            var claimPrincipalEmail = _ctxa.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            AppUser? user = _context.Users?.FirstOrDefault(x => x.Email == claimPrincipalEmail);
            return Task.FromResult(user);
        }
        public async Task<AppUser> SignUp(SignUpDTO signUpDTO) //Registering Account
        {
            var user = await _userManager.FindByEmailAsync(signUpDTO.Email);
            if (user == null)
                return await Task.FromResult<AppUser>(null);

            return user;
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
/* var user = await userManager.FindByEmailAsync(auth.Email);
          if (user == null)
              return Results.BadRequest(new 
              {
                  Code = new UnauthorizedResult().StatusCode,
                  Message = "Authorization error",
                  Detail = $"User : {auth.Email} Invalid UserName or Password"
              });
          var result = await signInManager.CheckPasswordSignInAsync(user, auth.Password, false);
          if (result.Succeeded)
          {
              var principal = await claimsPrincipalFactory.CreateAsync(user);
              var identity = principal.Identities.First();
              identity.AddClaim(new Claim("amr", "pwd"));
              identity.AddClaim(new Claim(ClaimTypes.Role, UserTypeAccess.Operator.Name));

              var handle = new JsonWebTokenHandler();
              var key = new RsaSecurityKey(keyManager.RsaKey);
              var token = handle.CreateToken(new SecurityTokenDescriptor()
              {
                  Issuer = "https://localhost:7154",
                  Subject = identity,
                  SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
              });

              return Results.Ok(new 
              {
                  Token = token  
              });
          }
          return Results.BadRequest(new 
          { 
              Code = new UnauthorizedResult().StatusCode,
              Message = "Authorization error",
              Detail = $"User : {auth.Email} Invalid UserName or Password"
          });
       */