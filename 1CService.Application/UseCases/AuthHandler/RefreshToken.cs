using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class RefreshToken : IRefreshToken
    {
        private readonly IAppUserService _appUserService;
        private readonly ITokenService _tokenService;

        public RefreshToken(IAppUserService appUserService, ITokenService tokenService) =>
            (_appUserService, _tokenService) = (appUserService, tokenService);

        public async Task<JwtAuthToken> Refresh(RefreshTokenQuery refreshToken)
        {
            var currentAppUser = await _appUserService.GetCurrentUser();
            if (currentAppUser == null)
                return default;

            var claims = await _appUserService.GetClaimsAndRoles();
            JwtAuthToken token = await _tokenService.RefreshToken(currentAppUser, refreshToken, claims);
            if (token.Equals(default(JwtAuthToken)))
                return new JwtAuthToken() { Error = "Erro refresh token" };
            return token;
        }
    }
}
