using _1CService.Application.Interfaces.Repositories;
using _1CService.Utilities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace _1CService.Controllers.Endpoints
{
    public static class TestPoint
    {
        public static IResult Handler(IHttpContextAccessor httpContextAccessor, IAppUserDbContext dbContext)
        {
            if(httpContextAccessor?.HttpContext?.User != null)
            {
                var user = httpContextAccessor?.HttpContext?.User;

                if (user?.Identity?.IsAuthenticated == true)
                {
                    var username = user.FindFirst(ClaimTypes.Name).Value;
                    var AppUser = dbContext.Users.SingleOrDefault(x=>x.UserName == username);
                    var claimsUser = user.Identities.GetClaims().Select(x => $"{x.Key} = {x.Value}").ToList();
                    return Results.Ok(new
                    {
                        Message = $"Authenticated identity :{username}",
                        Claims = claimsUser
                    });
                }
            }
            return Results.Ok($"Return no user auth");
        }
    }
}
