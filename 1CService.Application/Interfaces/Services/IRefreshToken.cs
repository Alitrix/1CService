using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Services
{
    public interface IRefreshToken
    {
        Task<JwtTokenDTO> Refresh(RefreshTokensDTO refreshToken);
    }
}