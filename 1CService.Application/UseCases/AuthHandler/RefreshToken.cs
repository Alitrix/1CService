using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;

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

        public async Task<JwtAuthToken> Refresh(RefreshTokenQuery refreshToken)
        {
            var currentAppUser = await _appUserService.GetCurrentUser();
            var claims = await _appUserService.GetClaimsAndRoles();
            JwtAuthToken token = await _tokenService.RefreshToken(currentAppUser, refreshToken, claims);
            if (token.Equals(default(JwtAuthToken)))
                return await Task.FromResult(new JwtAuthToken() { Error = "Erro refresh token" });
            return token;
        }
    }
}
