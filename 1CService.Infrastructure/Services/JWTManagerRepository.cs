using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Utilities;
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

        public JWTManagerRepository(KeyManager keyManager)
        {
            _keyManager = keyManager;
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
