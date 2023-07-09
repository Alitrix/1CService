using _1CService.Application.Interfaces.Services;
using _1CService.Application.Models;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;
using _1CService.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace _1CService.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly KeyManager _keyManager;
        private readonly AppTokenValidationParameters _tokenValidationParams;

        public TokenService(SignInManager<AppUser> signInManager, KeyManager keyManager, AppTokenValidationParameters tokenValidationParams)
        {
            _signInManager = signInManager;
            _keyManager = keyManager;
            _tokenValidationParams = tokenValidationParams;
        }
        public async Task<JwtAuthToken> RefreshToken(AppUser? appUser, RefreshTokenQuery refreshTokens, IList<Claim> claims)
        {
            if (IsValidLifetimeToken(refreshTokens.AccessToken))
                return new JwtAuthToken()
                {
                    Error = "Error request refresh token, while token is Valid"
                };

            if (appUser == null)
                return new JwtAuthToken()
                {
                    Error = "Error Email for token"
                };

            var oldRefreshToken = await _signInManager.UserManager.GetAuthenticationTokenAsync(appUser, 
                                                    "Bearer", "RefreshToken").ConfigureAwait(false);

            if (oldRefreshToken?.Equals(refreshTokens.RefreshToken) == false)
                return await Task.FromResult(new JwtAuthToken()
                {
                    Error = "Error token access"
                });

            var newTokenRefresh = GenerateToken(claims);
            if (newTokenRefresh.Equals(default(Tokens)))
                return new JwtAuthToken()
                {
                    Error = "Error generate token"
                };

            var retSetAuthToken = await _signInManager.UserManager.SetAuthenticationTokenAsync(appUser, 
                                                    "Bearer", "RefreshToken", newTokenRefresh.Refresh_Token).ConfigureAwait(false);
            if (retSetAuthToken == IdentityResult.Success)
                return new JwtAuthToken()
                {
                    Access_Tokens = newTokenRefresh,
                    TimeExp = TimeSpan.FromMinutes(1).Ticks,
                };
            return new JwtAuthToken()
            {
                Error = "Error save token"
            };
        }
        public string GenerateShortToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public Tokens GenerateToken(IList<Claim> claims)
        {
            try
            {
                var tokenHandler = new JsonWebTokenHandler();
                var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = AuthOptions.ISSUER,
                    Audience = AuthOptions.AUDIENCE,
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
                    
                    SigningCredentials = new SigningCredentials(new RsaSecurityKey(_keyManager.RsaKey), SecurityAlgorithms.RsaSsaPssSha256)
                });
                var refreshToken = GenerateShortToken();
                return new Tokens { Access_Token = token, Refresh_Token = refreshToken };
            }
            catch (Exception ex)
            {
                return default;
            }
        }
        public bool IsValidLifetimeToken(string token)
        {
            var tokenHandler = new JsonWebTokenHandler();
            TokenValidationResult validResult = tokenHandler.ValidateToken(token, _tokenValidationParams.tokenValidationParameters);
            
            if(!validResult.IsValid)
            {
                if(validResult.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsValidLifetimeToken(RefreshTokenQuery tokenRequest)
        {
            var jwtTokenHandler = new JsonWebTokenHandler();

            if (tokenRequest.AccessToken != null & jwtTokenHandler.CanReadToken(tokenRequest.AccessToken))
            {
                try
                {
                    var jwtSecurityToken = jwtTokenHandler.ReadJsonWebToken(tokenRequest.AccessToken);
                    
                    if (!lifetimeValidator(jwtSecurityToken.ValidFrom, jwtSecurityToken.ValidTo, null, _tokenValidationParams.tokenValidationParameters))
                        return false;
                    else
                        return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        readonly LifetimeValidator lifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
        {
            if (expires != null && notBefore != null)
            {
                var nowTime = DateTime.UtcNow;
                if (nowTime < expires.Value.ToUniversalTime() & nowTime > notBefore.Value.ToUniversalTime()) return true;
            }
            return false;
        };
    }
}
