using _1CService.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Controllers.Endpoints
{
    public static class TestPoint
    {
        public static async Task<IResult> Handler(IHttpContextAccessor httpContextAccessor, IAppUserDbContext dbContext)
        {
            if(httpContextAccessor?.HttpContext?.User != null)
            {
                var user = httpContextAccessor?.HttpContext?.User;

                if (user?.Identity?.IsAuthenticated == true)
                {
                    var claims = user.Claims.Select(x => KeyValuePair.Create(x.Type, x.Value));
                    StringBuilder sb = new StringBuilder();
                    foreach(var claim in claims) 
                    {
                        sb.AppendLine($"Type:{claim.Key},Value:{claim.Value}");
                    }

                    return await Task.FromResult(Results.Ok($"Authenticated identity :{user.Identity.Name}, Claims :{sb.ToString()}"));
                }
            }
            return await Task.FromResult(Results.Ok($"Return no user auth"));
        }
    }
}
