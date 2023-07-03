using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface IRefreshToken
    {
        Task<JwtTokenDTO> Refresh(RefreshTokensDTO refreshToken);
    }
}