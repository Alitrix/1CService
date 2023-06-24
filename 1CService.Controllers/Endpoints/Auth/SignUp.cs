using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.Auth
{
    public static class SignUp
    {
        public static async Task<IResult> Handler(ISignUpUser signUpUser, [FromBody] SignUpDTO signUpDTO)
        {
            return await signUpUser.CreateUser(signUpDTO) == null ? Results.NotFound(signUpDTO) : Results.Ok();
        }
    }
}