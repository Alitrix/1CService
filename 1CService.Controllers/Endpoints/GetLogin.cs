using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace _1CService.WebApi.Endpoints
{
    public static class GetLogin
    {
        public static async Task<IResult> Handler(ClaimsPrincipal user)
        {
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
