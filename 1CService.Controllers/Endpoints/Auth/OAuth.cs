using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.Auth
{
    public static class OAuth
    {
        public static async Task<IResult> RefreshTokenHandler(IRefreshToken refreshToken, [FromBody] RefreshTokensDTO tokenDto)
        {
            var userTmp = await refreshToken.Refresh(tokenDto);

            return Results.Ok(userTmp);
        }
        public static async Task<IResult> SignInHandler(ISignInUser signInUser, [FromBody] SignInDTO signInDTO)
        {
            var userTmp = await signInUser.Login(signInDTO);

            return Results.Ok(userTmp);
        }
        public static async Task<IResult> SignUpHandler(ISignUpUser signUpUser, [FromBody] SignUpDTO signUpDTO)
        {
            return await signUpUser.CreateUser(signUpDTO) == null ? Results.NotFound(signUpDTO) : Results.Ok();
        }
        public static async Task<IResult> SignOutHandler(ISignOutUser signOutUser)
        {
            return Results.Ok(await signOutUser.Logout());
        }
    }
}
