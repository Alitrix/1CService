using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Application.UseCases.Auth
{
    public class RefreshToken : IRefreshToken
    {
        private readonly IAuthenticateService _authenticateService;

        public RefreshToken(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        public async Task<JwtTokenDTO> Refresh(RefreshTokensDTO refreshToken)
        {
            JwtTokenDTO token = await _authenticateService.RefreshToken(refreshToken);
            if (token.Access_Tokens == null)
                return await Task.FromResult(new JwtTokenDTO() { Error = "Erro Sign" });
            return token;
        }
    }
}
