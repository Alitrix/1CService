using _1CService.Application.DTO;
using System.Security.Claims;

namespace _1CService.Application.Interfaces.Repositories
{
    public interface IJWTManagerRepository
    {
        Tokens GenerateToken(IList<Claim> claims);
        bool IsValidLifetimeToken(string token);
        bool IsValidLifetimeToken(RefreshTokensDTO tokenRequest);
    }
}
