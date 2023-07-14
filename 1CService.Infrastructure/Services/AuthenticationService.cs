using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using _1CService.Application.Interfaces.Services;
using _1CService.Domain.Enums;
using _1CService.Utilities;
using _1CService.Application.Enums;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Response;
using _1CService.Application.Models;
using _1CService.Application.DTO;

namespace _1CService.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticateService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly IRedisService _redisService;
        private readonly RedisConfiguration _redisConfiguration;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AuthenticationService(SignInManager<AppUser> signInManager,
                ITokenService tokenService,
                IAppUserService appUserService,
                IRedisService redisService,
                RedisConfiguration redisConfiguration,
                IPasswordHasher<AppUser> passwordHasher)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = signInManager.UserManager;
            _appUserService = appUserService;
            _redisService = redisService;
            _redisConfiguration = redisConfiguration;
            _passwordHasher = passwordHasher;
        }
       
        public async Task<PreRegistrationAppUserDTO?> SignUp(AppUser user, string password) //Pre Registering Account - Need add to Redis server
        {
            if (user == null && string.IsNullOrEmpty(password))
                return null;

            if (string.IsNullOrEmpty(user?.Email))
                return null;

            if (await _userManager.FindByEmailAsync(user.Email).ConfigureAwait(false) != null)
                return null;//exists

            user.SecurityStamp = RndGenerator.GenerateSecurityStamp();

            var token = await _signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
            var item = new PreRegistrationAppUserDTO()
            {
                EmailTokenConfirm = token,
                User = user,
                Password = _passwordHasher.HashPassword(user, password),
            };

            if (_redisService.Set(user.Id, item, _redisConfiguration.GetTimePreRegistrationUser()))
                return item;
            
            return null;

            /*IdentityResult createUserResult = await _userManager.CreateAsync(user, password).ConfigureAwait(false);
            if (createUserResult.Succeeded)
            {
                var principal = await _claimsPrincipalFactory.CreateAsync(user).ConfigureAwait(false);
                var identity = principal.Identities.First();

                createUserResult = await _userManager.AddToRoleAsync(user, UserTypeAccess.User).ConfigureAwait(false);
                if (createUserResult.Succeeded)
                {
                    createUserResult = await _userManager.AddClaimsAsync(user, identity.Claims).ConfigureAwait(false);
                    if (createUserResult.Succeeded)
                        return user;
                }
            }
            return null;*/
        }

        public async Task<JwtAuthToken> SignIn(SignInDTO signInDTO) //Autorization Account
        {
            if (string.IsNullOrEmpty(signInDTO.Email) && string.IsNullOrEmpty(signInDTO.Password))
                return new JwtAuthToken()
                {
                    //throw new RestException(HttpStatusCode.Unauthorized); // maybe need this package 
                    Error = "Invalid username or password."
                };
            
            AppUser? fndUser = await _userManager.FindByEmailAsync(signInDTO.Email).ConfigureAwait(false);
            if (fndUser is null)
                return new JwtAuthToken()
                {
                    Error = "Invalid username or password."
                };

            if(!await _userManager.IsEmailConfirmedAsync(fndUser).ConfigureAwait(false))
                return new JwtAuthToken()
                {
                    Error = "Email not Confirmed."
                };

            var signResult = await _signInManager.CheckPasswordSignInAsync(fndUser, signInDTO.Password, false).ConfigureAwait(false);
            if(!signResult.Succeeded)
                return new JwtAuthToken()
                {
                    Error = "Invalid username or password."
                };

            var claims = await _appUserService.GetClaimsAndRoles(fndUser).ConfigureAwait(false);

            var token = _tokenService.GenerateToken(claims);

            var retSetAuthToken = await _userManager.SetAuthenticationTokenAsync(fndUser, 
                                                       "Bearer", "RefreshToken", token.Refresh_Token).ConfigureAwait(false);
            if (!retSetAuthToken.Succeeded)
                return new JwtAuthToken()
                {
                    Error = "Invalid username or password."
                };

            return new JwtAuthToken()
            {
                Error = "No error",
                Access_Tokens = token,
                TimeExp = TimeSpan.FromMinutes(1).Ticks
            };
        }
        
        public async Task<SignOut> SignOut() // Exit Account
        {
            if(_appUserService.IsAuthenticate() == false)
                return new SignOut()
                {
                    Message = "Error SignOut"
                }; 
            
            await _signInManager.SignOutAsync().ConfigureAwait(false);

            AppUser? user = await _appUserService.GetCurrentUser().ConfigureAwait(false);
            if (user == null)
                return new SignOut()
                {
                    Message = "Error"
                };

            var currentRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, 
                                                            "Bearer", "RefreshToken").ConfigureAwait(false);
            if (currentRefreshToken != null)
                await _userManager.SetAuthenticationTokenAsync(user, 
                                                            "Bearer", "RefreshToken", string.Empty).ConfigureAwait(false);

            return new SignOut()
            {
                Message = "SignOut"
            };
        }
    }
}