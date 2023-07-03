using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace _1CService.Infrastructure.Services
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly KeyManager _keyManager;
        AppTokenValidationParameters _tokenValidationParams;

        public JWTManagerRepository(KeyManager keyManager, AppTokenValidationParameters tokenValidationParams)
        {
            _keyManager = keyManager;
            _tokenValidationParams = tokenValidationParams;
        }
        public Tokens GenerateToken(IList<Claim> claims)
        {
            return GenerateJWTTokens(claims);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        
        private Tokens GenerateJWTTokens(IList<Claim> claims)
        {
            try
            {
                var tokenHandler = new JsonWebTokenHandler();
                var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = AuthOptions.ISSUER,
                    Audience = AuthOptions.AUDIENCE,
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(15),
                    
                    SigningCredentials = new SigningCredentials(new RsaSecurityKey(_keyManager.RsaKey), SecurityAlgorithms.RsaSsaPssSha256)
                });
                var refreshToken = GenerateRefreshToken();
                return new Tokens { Access_Token = token, Refresh_Token = refreshToken };
            }
            catch (Exception ex)
            {
                return null;
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
        public bool IsValidLifetimeToken(RefreshTokensDTO tokenRequest)
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
        LifetimeValidator lifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
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
