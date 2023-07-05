using _1CService.Application.DTO;
using _1CService.Application.Enums;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Controllers.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.AuthEP
{
    public static class OAuth
    {
        public static async Task<IResult> RefreshTokenHandler(IRefreshToken refreshToken, [FromBody] RefreshTokenDTO tokenDto)
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
        public static async Task<IResult> RoleAddHandler(IRoleAddToUser roleAddToUser, string guid)
        {
            var addRoleToUser = await roleAddToUser.AddRole(guid);
            return Results.Ok(addRoleToUser);
        }
        public static async Task<string> RequestAddRoleManagerHandler(IGenerateRoleGuid requestGenerationRole)
        {
            var response = await requestGenerationRole.Generate(UserTypeAccess.Manager);
            return response.Message;
        }
    }
}
