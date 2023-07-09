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
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsPrincipalFactory;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;

        public AuthenticationService(SignInManager<AppUser> signInManager,
                ITokenService tokenService,
                IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory, 
                UserManager<AppUser> userManager,
                IAppUserService appUserService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _userManager = userManager;
            _appUserService = appUserService;
        }

       
        public async Task<AppUser?> SignUp(AppUser user, string password) //Registering Account
        {
            if (user == null && string.IsNullOrEmpty(password))
                return null;

            if (string.IsNullOrEmpty(user?.Email))
                return null;

            if (await _signInManager.UserManager.FindByEmailAsync(user.Email).ConfigureAwait(false) != null)
                return await Task.FromResult<AppUser?>(null).ConfigureAwait(false);//exists

            user.UserName = user.Email;
            user.Id = Guid.NewGuid().ToString();
            user.SecurityStamp = RndGenerator.GenerateSecurityStamp();
            user.User1C = "";
            user.WorkPlace = WorkPlace.None;
            user.Password1C = "";


            IdentityResult createUserResult = await _signInManager.UserManager.CreateAsync(user, password).ConfigureAwait(false);
            if (createUserResult.Succeeded)
            {
                var principal = await _claimsPrincipalFactory.CreateAsync(user).ConfigureAwait(false);
                var identity = principal.Identities.First();

                createUserResult = await _signInManager.UserManager.AddToRoleAsync(user, UserTypeAccess.User).ConfigureAwait(false);
                if (createUserResult.Succeeded)
                {
                    createUserResult = await _signInManager.UserManager.AddClaimsAsync(user, identity.Claims).ConfigureAwait(false);
                    if (createUserResult.Succeeded)
                        return user;
                }
            }
            return null;
        }

        public async Task<JwtAuthToken> SignIn(SignInDTO signInDTO) //Autorization Account
        {
            if (string.IsNullOrEmpty(signInDTO.Email) && string.IsNullOrEmpty(signInDTO.Password))
                return new JwtAuthToken()
                {
                    //throw new RestException(HttpStatusCode.Unauthorized); // maybe need this package 
                    Error = "Invalid username or password."
                };
            
            AppUser? fndUser = await _signInManager.UserManager.FindByEmailAsync(signInDTO.Email).ConfigureAwait(false);
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

            var retSetAuthToken = await _signInManager.UserManager.SetAuthenticationTokenAsync(fndUser, 
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

            var currentRefreshToken = await _signInManager.UserManager.GetAuthenticationTokenAsync(user, 
                                                            "Bearer", "RefreshToken").ConfigureAwait(false);
            if (currentRefreshToken != null)
                await _signInManager.UserManager.SetAuthenticationTokenAsync(user, 
                                                            "Bearer", "RefreshToken", string.Empty).ConfigureAwait(false);

            return new SignOut()
            {
                Message = "SignOut"
            };
        }
    }
}