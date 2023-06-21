using _1CService.Application.DTO;
using _1CService.Application.UseCases.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.Auth
{
    public static class SignUp
    {
        public static async Task<IResult> Handler(ISignUpUser signUpUser, [FromBody] SignUpDTO auth)
        {
            return Results.Ok(await signUpUser.Create(auth));
        }
    }
}