using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;
using _1CService.Controllers.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.TokenEP
{
    public static class Token
    {
        public static async Task<IResult> RefreshTokenHandler(IRefreshToken refreshToken, [FromBody] RefreshTokenDTO tokenDto)
        {
            JwtAuthToken userTmp = await refreshToken.Refresh(new RefreshTokenQuery()
            {
                AccessToken = tokenDto.AccessToken,
                RefreshToken = tokenDto.RefreshToken,
                Email = tokenDto.Email,
            });

            return Results.Ok(userTmp);
        }
    }
}
