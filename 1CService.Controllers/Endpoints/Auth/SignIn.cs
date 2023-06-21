using _1CService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace _1CService.Controllers.Endpoints.Auth
{
    public static class SignIn
    {
        public static async Task<IResult> Handler(ClaimsPrincipal user, IAuthenticateService authenticateService)
        {
            var userTmp = authenticateService.GetCurrentUser();
            var lstClaims = user.Claims.Select(x => KeyValuePair.Create(x.Type, x.Value));
            var msg = "Get Login response ";

            return await Task.FromResult(Results.Ok(new
            {
                Msg = msg,
                Claims = lstClaims
            }));
        }
    }
}
