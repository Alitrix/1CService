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
            var currentUser = await GetCurrentUser().ConfigureAwait(false);
            if(currentUser == null)
                return new List<Claim>();
            var identitys = await _userManager.GetClaimsAsync(currentUser).ConfigureAwait(false);
            return identitys.ToList();
        }
        public async Task<AppUser> SignUp(AppUser user, string password) //Registering Account
        {
            if (user == null && string.IsNullOrEmpty(password))
                return await Task.FromResult<AppUser>(null).ConfigureAwait(false);

            if (await _userManager.FindByEmailAsync(user.Email) != null)
                return await Task.FromResult<AppUser>(null).ConfigureAwait(false);//exists

            user.UserName = user.Email;
            user.Id = Guid.NewGuid().ToString();
            user.SecurityStamp = RndGenerator.GenerateSecurityStamp();
            user.User1C = user.Email;
            user.WorkPlace = WorkPlace.None;
            user.Password1C = "None";


            IdentityResult createUserResult = await _userManager.CreateAsync(user, password).ConfigureAwait(false);
            if (createUserResult.Succeeded)
            {
                var principal = await _claimsPrincipalFactory.CreateAsync(user).ConfigureAwait(false);
                var identity = principal.Identities.First();

                createUserResult = await _userManager.AddToRoleAsync(user, UserTypeAccess.User);
                if (createUserResult.Succeeded)
                {
                    createUserResult = await _userManager.AddClaimsAsync(user, identity.Claims).ConfigureAwait(false);
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
            var roles = await _userManager.GetRolesAsync(fndUser);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var token = GenerateJWTTokens(claims);

            var retSetAuthToken = await _signInManager.UserManager.SetAuthenticationTokenAsync(fndUser, fndUser.UserName, "Token", token);
            return new JwtTokenDTO()
            {
                Error = "No error",
                Access_Tokens = token,
                TimeExp = TimeSpan.FromMinutes(1).Ticks
            };
        }
        public Task<IdentityResult> SignOut(AppUser user) // Exit Account
        {
            throw new NotImplementedException();
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public Tokens GenerateJWTTokens(IList<Claim> claims)
        {
            try
            {
                var tokenHandler = new JsonWebTokenHandler();
                var token = tokenHandler.CreateToken( new SecurityTokenDescriptor
                {
                    Issuer = AuthOptions.ISSUER,
                    Audience = AuthOptions.AUDIENCE,
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new RsaSecurityKey(_keyManager.RsaKey), SecurityAlgorithms.RsaSha256)
                });
                var refreshToken = GenerateRefreshToken();
                return new Tokens { Access_Token = token, Refresh_Token = refreshToken };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}