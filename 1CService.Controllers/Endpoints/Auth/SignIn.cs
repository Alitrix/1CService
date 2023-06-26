using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.Auth
{
    public static class SignIn
    {
        [AllowAnonymous]
        public static async Task<IResult> Handler(ISignInUser signInUser, [FromBody] SignInDTO signInDTO)
        {
            var userTmp = await signInUser.Login(signInDTO);

            return Results.Ok(userTmp);
        }
    }
}
