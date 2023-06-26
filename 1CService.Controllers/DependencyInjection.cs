using _1CService.Persistence.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using _1CService.Controllers.Endpoints;
using _1CService.Controllers.Endpoints.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using _1CService.Application.DTO;
using Microsoft.AspNetCore.Http;

namespace _1CService.Controllers
{
    public static class DependencyInjection
    {
        public static WebApplication Add1CServiceEndpoints(this WebApplication app)
        {
            app.MapGet("/", (ClaimsPrincipal user) => user.Claims.Select(x => KeyValuePair.Create(x.Type, x.Value)))
                .RequireAuthorization(UserTypeAccess.User, UserTypeAccess.Administrator);
            
            app.MapGet("/test", TestPoint.Handler).RequireAuthorization(UserTypeAccess.User);
            app.MapGet("/AddAdmin", async (UserManager<AppUser> userManager, HttpContext ctx) =>
            {
                var usr = await userManager.FindByNameAsync(ctx.User.FindFirstValue(ClaimTypes.Name));
                await userManager.AddClaimAsync(usr, new Claim(ClaimTypes.Role, UserTypeAccess.Administrator));
            });
            app.MapGet("/sign-up", SignUp.Handler).AllowAnonymous();
            app.MapGet("/sign-in", SignIn.Handler).AllowAnonymous();
            app.MapGet("/sign-out", SignOut.Handler).RequireAuthorization(UserTypeAccess.User);


            return app;
        }

        public static IServiceCollection Add1CServiceRoles(this IServiceCollection services)
        {
            services.AddAuthorization(b =>
            {
                b.AddPolicy(UserTypeAccess.User, pb => pb
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(ClaimTypes.Role, UserTypeAccess.User)
                );
                b.AddPolicy(UserTypeAccess.Manager, pb => pb
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(ClaimTypes.Role, UserTypeAccess.Manager)
                );
                b.AddPolicy(UserTypeAccess.Administrator, pb => pb
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(ClaimTypes.Role, UserTypeAccess.Administrator)
                );
            });
            return services;
        }
    }
}