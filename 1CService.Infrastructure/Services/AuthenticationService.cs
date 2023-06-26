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
using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace _1CService.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticateService
    {
        private readonly IHttpContextAccessor _ctxa;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly KeyManager _keyManager;
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsPrincipalFactory;

        public AuthenticationService(IHttpContextAccessor ctxa, 
                SignInManager<AppUser> signInManager,
                UserManager<AppUser> userManager,
                KeyManager keyManager,
                IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory)
        {
            _ctxa = ctxa;
            _signInManager = signInManager;
            _userManager = userManager;
            _keyManager = keyManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        public async Task<AppUser?> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(_ctxa.HttpContext.User.FindFirstValue(ClaimTypes.Name));
            return await Task.FromResult(user);
        }
        public async Task<List<Claim>> GetCurrentClaims()
        {
            var currentUser = await GetCurrentUser();
            if(currentUser == null)
                return new List<Claim>();
            var identitys = await _userManager.GetClaimsAsync(currentUser);
            return identitys.ToList();
        }
        public async Task<AppUser> SignUp(AppUser user, string password) //Registering Account
        {
            if (user == null && string.IsNullOrEmpty(password))
                return await Task.FromResult<AppUser>(null);

            if (await _userManager.FindByEmailAsync(user.Email) != null)
                return await Task.FromResult<AppUser>(null);//exists

            user.Id = Guid.NewGuid().ToString();
            user.SecurityStamp = RndGenerator.GenerateSecurityStamp();
            user.User1C = "";
            user.WorkPlace = WorkPlace.None;
            user.Password1C = "None";


            IdentityResult createUserResult = await _userManager.CreateAsync(user, password).ConfigureAwait(false);
            if (createUserResult.Succeeded)
            {
                var principal = await _claimsPrincipalFactory.CreateAsync(user);
                var identity = principal.Identities.First();
                identity.AddClaim(new Claim(ClaimTypes.Role, UserTypeAccess.User));

                createUserResult = await _userManager.AddClaimsAsync(user, identity.Claims).ConfigureAwait(false);

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
                    //throw new RestException(HttpStatusCode.Unauthorized); // maybe need this package 
                    Error = "Invalid username or password."
                });
            
            AppUser? fndUser = await _userManager.FindByEmailAsync(signInDTO.Email);
            if (fndUser is null)
                return await Task.FromResult(new JwtTokenDTO()
                {
                    Error = "Invalid username or password."
                });

            var signResult = await _signInManager.CheckPasswordSignInAsync(fndUser, signInDTO.Password, false);
            if(!signResult.Succeeded)
                return await Task.FromResult(new JwtTokenDTO()
                {
                    Error = "Invalid username or password."
                });
            var claims = await _userManager.GetClaimsAsync(fndUser);
            
            var handle = new JsonWebTokenHandler();

            var key = new RsaSecurityKey(_keyManager.RsaKey);
            var token = handle.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = AuthOptions.ISSUER,
                Audience = AuthOptions.AUDIENCE,
                Subject = new ClaimsIdentity(claims),
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