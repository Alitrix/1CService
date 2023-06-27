using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.Auth
{
    public class RefreshToken
    {
        public static async Task<IResult> Handler(IRefreshToken refreshToken, [FromBody] RefreshTokensDTO tokenDto)
        {
            var userTmp = await refreshToken.Refresh(tokenDto);

            return Results.Ok(userTmp);
        }
    }
}
