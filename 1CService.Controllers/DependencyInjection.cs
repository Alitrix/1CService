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
        public static WebApplication AddEndpoints(this WebApplication app)
        {
            app.MapGet("/", (ClaimsPrincipal user) => user.Claims.Select(x => KeyValuePair.Create(x.Type, x.Value))).RequireAuthorization(UserTypeAccess.Operator.Name, "amr");
            app.MapGet("/test", TestPoint.Handler).RequireAuthorization("amr");
            
            app.MapGet("/sign-up", SignUp.Handler).AllowAnonymous();
            app.MapGet("/sign-in", SignIn.Handler).AllowAnonymous();
            
            
            return app;
        }

        public static IServiceCollection AddRoles(this IServiceCollection services)
        {
            services.AddAuthorization(b =>
            {
                b.AddPolicy("operator", pb => pb
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireClaim(ClaimTypes.Role, UserTypeAccess.Operator.Name)
                );
                b.AddPolicy("amr", pb => pb
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireClaim("amr", "pwd")
                );
            });
            return services;
        }
    }
}