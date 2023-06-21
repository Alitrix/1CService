using _1CService.Application.DTO;
using _1CService.Persistence.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using _1CService.Controllers.Endpoints;
using _1CService.Utilities;
using _1CService.Application.Interfaces.Services;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using _1CService.Controllers.Endpoints.Auth;

namespace _1CService.Controllers
{
    public static class DependencyInjection
    {
        public static WebApplication AddEndpoints(this WebApplication app)
        {
            //Authorization
            app.MapGet("/Login", GetLogin.Handler).AllowAnonymous();

            app.MapGet("/", (ClaimsPrincipal user) => user.Claims.Select(x => KeyValuePair.Create(x.Type, x.Value))).RequireAuthorization(UserTypeAccess.Operator.Name, "amr");
            app.MapGet("/secret", () => "secret");
            app.MapGet("/test", TestPoint.Handler).RequireAuthorization("amr");

            //Registering
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