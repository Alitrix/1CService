using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;

namespace _1CService.Application.UseCases.Auth
{
    public class RefreshToken : IRefreshToken
    {
        private readonly IAppUserService _appUserService;
        private readonly ITokenService _tokenService;

        public RefreshToken(IAppUserService appUserService, ITokenService tokenService)
        {
            _appUserService = appUserService;
            _tokenService = tokenService;
        }

        public async Task<JwtTokenDTO> Refresh(RefreshTokensDTO refreshToken)
        {
            var currentAppUser = await _appUserService.GetCurrentUser();
            var claims = await _appUserService.GetClaimsAndRoles();
            JwtTokenDTO token = await _tokenService.RefreshToken(currentAppUser, refreshToken, claims);
            if (token == null)
                return await Task.FromResult(new JwtTokenDTO() { Error = "Erro refresh token" });
            return token;
        }
    }
}
