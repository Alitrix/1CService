using _1CService.Application.Enums;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Request;
using _1CService.Controllers.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.AuthEP
{
    public static class OAuth
    {
        public static async Task<IResult> RefreshTokenHandler(IRefreshToken refreshToken, [FromBody] RefreshTokenDTO tokenDto)
        {
            var userTmp = await refreshToken.Refresh(new RefreshTokenQuery()
            {
                AccessToken = tokenDto.AccessToken,
                RefreshToken = tokenDto.RefreshToken,
                Email = tokenDto.Email,
            });

            return Results.Ok(userTmp);
        }
        public static async Task<IResult> SignInHandler(ISignInUser signInUser, [FromBody] SignInDTO signInDTO)
        {
            var userTmp = await signInUser.Login(new SignInQuery()
            {
                Email = signInDTO.Email,
                Password = signInDTO.Password,
            });

            return Results.Ok(userTmp);
        }
        public static async Task<IResult> SignUpHandler(ISignUpUser signUpUser, [FromBody] SignUpDTO signUpDTO)
        {
            return await signUpUser.CreateUser(new SignUpQuery()
            {
                Email = signUpDTO.Email, 
                Password = signUpDTO.Password,
                UserName = signUpDTO.UserName,
            }) == null ? Results.NotFound(signUpDTO) : Results.Ok();
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
