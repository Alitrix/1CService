using _1CService.Persistence.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using _1CService.Controllers.Endpoints;
using _1CService.Controllers.Endpoints.Auth;
using Microsoft.AspNetCore.Authorization;

namespace _1CService.Controllers
{
    public static class DependencyInjection
    {
        public static WebApplication Add1CServiceEndpoints(this WebApplication app)
        {
            app.MapGet("/", (ClaimsPrincipal user) => user.Claims.Select(x => KeyValuePair.Create(x.Type, x.Value))).RequireAuthorization(UserTypeAccess.Manager.Name);
            
            app.MapGet("/test", TestPoint.Handler).RequireAuthorization(UserTypeAccess.User.Name);
            
            app.MapGet("/sign-up", SignUp.Handler).AllowAnonymous();
            app.MapGet("/sign-in", SignIn.Handler).AllowAnonymous();
            app.MapGet("/sign-out", SignOut.Handler).RequireAuthorization(UserTypeAccess.User.Name);


            return app;
        }

        public static IServiceCollection Add1CServiceRoles(this IServiceCollection services)
        {
            services.AddAuthorization(b =>
            {
                b.AddPolicy(UserTypeAccess.User.Name, pb => pb
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                );
                b.AddPolicy(UserTypeAccess.Manager.Name, pb => pb
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(ClaimTypes.Role, UserTypeAccess.Manager.Name)
                );
                b.AddPolicy(UserTypeAccess.Administrator.Name, pb => pb
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(ClaimTypes.Role, UserTypeAccess.Administrator.Name)
                );
            });
            return services;
        }
    }
}