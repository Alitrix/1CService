using _1CService.Application.DTO;
using _1CService.Application.UseCases.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.Auth
{
    public static class SignUp
    {
        public static async Task<IResult> Handler(ISignUpUser signUpUser, [FromBody] SignUpDTO signUpDTO)
        {
            var user = await signUpUser.Create(signUpDTO);
            if (user != null)
                return Results.Ok();
            else
                return Results.NotFound(signUpDTO);
        }
    }
}