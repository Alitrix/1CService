using _1CService.Application.DTO;
using System.Security.Claims;

namespace _1CService.Application.Interfaces.Repositories
{
    public interface IJWTManagerRepository
    {
        Tokens GenerateToken(string userName);
        Tokens GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
