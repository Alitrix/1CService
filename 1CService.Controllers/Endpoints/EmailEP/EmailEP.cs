using _1CService.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.EmailEP
{
    public static class EmailEP
    {
        public static async Task<IResult> EmailTokenValidation(IEmailConfirmUser emailConfirmUser, [FromQuery] string userid, [FromQuery] string token)
        {
            return Results.Ok(await emailConfirmUser.Validation(userid, token));
        }
        public static async Task<IResult> EmailResendConfirm(HttpContext context, [FromQuery] string userid)
        {
            return Results.Ok("Resend");
        }
    }
}
