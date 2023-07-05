using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;
using _1CService.Controllers.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.AuthEP
{
    public static class OAuth
    {
        public static async Task<IResult> SignInHandler(ISignInUser signInUser, [FromBody] SignInDTO signInDTO)
        {
            JwtAuthToken userTmp = await signInUser.Login(new SignInQuery()
            {
                Email = signInDTO.Email,
                Password = signInDTO.Password,
            });

            return Results.Ok(userTmp);
        }
        public static async Task<IResult> SignUpHandler(ISignUpUser signUpUser, [FromBody] SignUpDTO signUpDTO)
        {
            SignUp? signUp = await signUpUser.CreateUser(new SignUpQuery()
            {
                Email = signUpDTO.Email,
                Password = signUpDTO.Password,
                UserName = signUpDTO.UserName,
            });

            return signUp == null ? Results.NotFound(signUpDTO) : Results.Ok();
        }
        public static async Task<IResult> SignOutHandler(ISignOutUser signOutUser)
        {
            SignOut logOut = await signOutUser.Logout();

            return Results.Ok(logOut);
        }
    }
}
