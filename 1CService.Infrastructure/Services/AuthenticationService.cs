using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Interfaces.Services;
using _1CService.Domain.Enums;
using _1CService.Utilities;
using _1CService.Persistence.Enums;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<AppUser> SignUp(AppUser user, string password) //Registering Account
        {
            if (user == null && string.IsNullOrEmpty(password))
                return await Task.FromResult<AppUser>(null); // Validate dto datamodel

            user.Id = Guid.NewGuid().ToString();
            user.SecurityStamp = RndGenerator.GenerateSecurityStamp();
            user.User1C = "";
            user.WorkPlace = WorkPlace.None;
            user.Password1C = "None";

            if (await _userManager.FindByEmailAsync(user.Email) != null)
                return await Task.FromResult<AppUser>(null);//Alright exists


            IdentityResult createUserResult = await _userManager.CreateAsync(user, password).ConfigureAwait(false);
            if (createUserResult.Succeeded)
            {
                createUserResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, UserTypeAccess.User.Name)).ConfigureAwait(false);
                if (createUserResult.Succeeded)
                    return user;
                else
                    return await Task.FromResult<AppUser>(null);
            }

            return user;
        }

        public async Task<JwtTokenDTO> SignIn(SignInDTO signInDTO) //Autorization Account
        {
            if (string.IsNullOrEmpty(signInDTO.Email) && string.IsNullOrEmpty(signInDTO.Password))
                return await Task.FromResult(new JwtTokenDTO()
                {
                    Error = "Error username or password"
                });
            AppUser fndUser = await _userManager.FindByEmailAsync(signInDTO.Email);
            if (fndUser is null)
                return await Task.FromResult(new JwtTokenDTO()
                {
                    Error = "Error username or password"
                });
            var createUserResult = await _userManager.AddClaimAsync(fndUser, new Claim(ClaimTypes.Role, UserTypeAccess.User.Name)).ConfigureAwait(false);
            
            KeyManager keyManager = new KeyManager();

            var handle = new JsonWebTokenHandler();

            var principal = await _claimsPrincipalFactory.CreateAsync(fndUser);
            var identity = principal.Identities.First();

            var key = new RsaSecurityKey(keyManager.RsaKey);
            var token = handle.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = "https://localhost:7154",
                Subject = identity,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
            });

            return new JwtTokenDTO()
            {
                Error = "No error",
                Token = token,
                TimeExp = TimeSpan.FromSeconds(240).Ticks
            };
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