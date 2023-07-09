using _1CService.Application.Models;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;
using System.Security.Claims;

namespace _1CService.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Tokens GenerateToken(IList<Claim> claims);
        Task<JwtAuthToken> RefreshToken(AppUser? appUser, RefreshTokenQuery refreshTokens, IList<Claim> claims);
        bool IsValidLifetimeToken(string token);
        bool IsValidLifetimeToken(RefreshTokenQuery tokenRequest);
        string GenerateShortToken();
    }
}
