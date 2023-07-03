using _1CService.Application.DTO;
using System.Security.Claims;

namespace _1CService.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Tokens GenerateToken(IList<Claim> claims);
        Task<JwtTokenDTO> RefreshToken(AppUser? appUser, RefreshTokensDTO refreshTokens, IList<Claim> claims);
        bool IsValidLifetimeToken(string token);
        bool IsValidLifetimeToken(RefreshTokensDTO tokenRequest);
    }
}
