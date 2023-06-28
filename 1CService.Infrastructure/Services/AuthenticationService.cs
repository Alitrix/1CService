using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using _1CService.Application.Interfaces.Services;
using _1CService.Domain.Enums;
using _1CService.Utilities;
using _1CService.Persistence.Enums;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using _1CService.Application.Interfaces.Repositories;

namespace _1CService.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticateService
    {
        private readonly IHttpContextAccessor _ctxa;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTManagerRepository _jwtManagerRepository;
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsPrincipalFactory;

        public AuthenticationService(IHttpContextAccessor ctxa, 
                SignInManager<AppUser> signInManager,
                IJWTManagerRepository jwtManagerRepository,
                IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory)
        {
            _ctxa = ctxa;
            _signInManager = signInManager;
            _jwtManagerRepository = jwtManagerRepository;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        public async Task<AppUser?> GetCurrentUser()
        {
            var user = await _signInManager.UserManager.FindByNameAsync(_ctxa.HttpContext.User.FindFirstValue(ClaimTypes.Name));
            return await Task.FromResult(user);
        }
        public async Task<IList<Claim>> GetCurrentClaims()
        {
            var currentUser = await GetCurrentUser().ConfigureAwait(false);
            if(currentUser == null)
                return new List<Claim>();

            var claims = await _signInManager.UserManager.GetClaimsAsync(currentUser);
            var roles = await _signInManager.UserManager.GetRolesAsync(currentUser);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }
        public async Task<AppUser> SignUp(AppUser user, string password) //Registering Account
        {
            if (user == null && string.IsNullOrEmpty(password))
                return await Task.FromResult<AppUser>(null).ConfigureAwait(false);

            if (await _signInManager.UserManager.FindByEmailAsync(user.Email) != null)
                return await Task.FromResult<AppUser>(null).ConfigureAwait(false);//exists

            user.UserName = user.Email;
            user.Id = Guid.NewGuid().ToString();
            user.SecurityStamp = RndGenerator.GenerateSecurityStamp();
            user.User1C = user.Email;
            user.WorkPlace = WorkPlace.None;
            user.Password1C = "None";


            IdentityResult createUserResult = await _signInManager.UserManager.CreateAsync(user, password).ConfigureAwait(false);
            if (createUserResult.Succeeded)
            {
                var principal = await _claimsPrincipalFactory.CreateAsync(user).ConfigureAwait(false);
                var identity = principal.Identities.First();

                createUserResult = await _signInManager.UserManager.AddToRoleAsync(user, UserTypeAccess.User);
                if (createUserResult.Succeeded)
                {
                    createUserResult = await _signInManager.UserManager.AddClaimsAsync(user, identity.Claims).ConfigureAwait(false);
                    if (createUserResult.Succeeded)
                        return user;
                }
            }
            return await Task.FromResult<AppUser>(null).ConfigureAwait(false);
        }

        public async Task<JwtTokenDTO> SignIn(SignInDTO signInDTO) //Autorization Account
        {
            if (string.IsNullOrEmpty(signInDTO.Email) && string.IsNullOrEmpty(signInDTO.Password))
                return await Task.FromResult(new JwtTokenDTO()
                {
                    //throw new RestException(HttpStatusCode.Unauthorized); // maybe need this package 
                    Error = "Invalid username or password."
                });
            
            AppUser? fndUser = await _signInManager.UserManager.FindByEmailAsync(signInDTO.Email);
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
            
            var claims = await _signInManager.UserManager.GetClaimsAsync(fndUser);
            var roles = await _signInManager.UserManager.GetRolesAsync(fndUser);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            claims.Add(new Claim(ClaimTypes.Role, UserTypeAccess.Manager));

            var token = _jwtManagerRepository.GenerateToken(claims);

            var retSetAuthToken = await _signInManager.UserManager.SetAuthenticationTokenAsync(fndUser, fndUser.UserName, "RefreshToken", token.Refresh_Token);
            return new JwtTokenDTO()
            {
                Error = "No error",
                Access_Tokens = token,
                TimeExp = TimeSpan.FromMinutes(1).Ticks
            };
        }
        public async Task<JwtTokenDTO> RefreshToken(RefreshTokensDTO refreshTokens)
        {
            if(_jwtManagerRepository.IsValidLifetimeToken(refreshTokens.AccessToken))
                return await Task.FromResult(new JwtTokenDTO()
                {
                    Error = "Error request refresh token, whele token is Valid"
                });
                        
            AppUser? appUser = await _signInManager.UserManager.FindByEmailAsync(refreshTokens.Email);
            
            if(appUser == null)
                return await Task.FromResult(new JwtTokenDTO()
                {
                    Error = "Error Email for token"
                });

            var oldRefreshToken = await _signInManager.UserManager.GetAuthenticationTokenAsync(appUser, appUser.UserName, "RefreshToken");
            
            if (oldRefreshToken?.Equals(refreshTokens.RefreshToken) == false)
                return await Task.FromResult(new JwtTokenDTO()
                {
                    Error = "Error token access"
                });

            var claims = await _signInManager.UserManager.GetClaimsAsync(appUser);
            var roles = await _signInManager.UserManager.GetRolesAsync(appUser);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var newTokenRefresh = _jwtManagerRepository.GenerateToken(claims);
            if(newTokenRefresh == null)
                return await Task.FromResult(new JwtTokenDTO()
                {
                    Error = "Error generate token"
                });

            var retSetAuthToken = await _signInManager.UserManager.SetAuthenticationTokenAsync(appUser, appUser.UserName, "RefreshToken", newTokenRefresh.Refresh_Token);
            if (retSetAuthToken == IdentityResult.Success)
                return new JwtTokenDTO()
                {
                    Access_Tokens = newTokenRefresh,
                    TimeExp = TimeSpan.FromMinutes(1).Ticks,
                };
            return await Task.FromResult(new JwtTokenDTO()
            {
                Error = "Error save token"
            });
        }
        public Task<IdentityResult> SignOut(AppUser user) // Exit Account
        {
            throw new NotImplementedException();
        }
    }
}