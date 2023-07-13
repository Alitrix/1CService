using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

using _1CService.Application.Enums;
using _1CService.Application.Interfaces;
using _1CService.Controllers.Endpoints.AuthEP;
using _1CService.Controllers.Endpoints.BlankOrdersEP;
using _1CService.Controllers.Endpoints.ProfileEP;
using _1CService.Controllers.Endpoints.TokenEP;
using _1CService.Controllers.Endpoints.RoleEP;
using _1CService.Controllers.Endpoints.EmailEP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using _1CService.Application.Models;

namespace _1CService.Controllers
{
    public static class DependencyInjection
    {
        public static WebApplication AddEndpoints(this WebApplication app)
        {
            //Welcome Part:)
            app.MapGet("/", () => "Hello from ASP.NET Core Microservice");   
            
            //Auth
            app.MapPost("/oauth/register", OAuth.SignUpHandler).AllowAnonymous();
            app.MapPost("/oauth/login", OAuth.SignInHandler).AllowAnonymous();
            app.MapGet("/oauth/logout", OAuth.SignOutHandler).RequireAuthorization(UserTypeAccess.User);

            //Mail
            app.MapGet("/email/confirm", EmailEP.EmailTokenValidation).AllowAnonymous().WithName("email-confirm");
            app.MapGet("/email/resend-confirm", EmailEP.EmailResendConfirm).AllowAnonymous().WithName("email-resend");

            //Token
            app.MapPost("/jwt/refresh-token", Token.RefreshTokenHandler).AllowAnonymous();

            //Role
            app.MapGet("/role/add-role-accept", Role.RoleAddHandler).AllowAnonymous().WithName("add-role-accept");
            app.MapPost("/role/add-role-denied", Role.RoleAddHandler).AllowAnonymous().WithName("add-role-denied");//Need change method action to Delete request from db
            app.MapGet("/role/request-role-manager", Role.RequestAddRoleManagerHandler).RequireAuthorization(UserTypeAccess.User);

            //Profile
            app.MapGet("/profile/user", Profile.GetProfile).RequireAuthorization(UserTypeAccess.Manager);
            app.MapPost("/profile/user", Profile.SetProfile).RequireAuthorization(UserTypeAccess.Manager);


            //1C Service
            app.MapGet("/blankorder/list", BlankOrderEP.GetListBlankOrderHandler).RequireAuthorization(UserTypeAccess.Manager);
            app.MapPost("/blankorder/detail", BlankOrderEP.GetBlankOrderDetailHandler).RequireAuthorization(UserTypeAccess.Manager);
            app.MapPost("/blankorder/accept-inwork", BlankOrderEP.AcceptInWorkBlankOrderHandler).RequireAuthorization(UserTypeAccess.Manager);
            app.MapPost("/blankorder/add-comment", BlankOrderEP.AddCommentToBlankOrderHandler).RequireAuthorization(UserTypeAccess.Manager);

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

/*app.Use(async (context, next) =>
{
    var currentEndpoint = context.GetEndpoint();

    if (currentEndpoint is null)
    {
        await next(context);
        return;
    }

    Console.WriteLine($"Endpoint: {currentEndpoint.DisplayName}");

    if (currentEndpoint is RouteEndpoint routeEndpoint)
    {
        Console.WriteLine($"  - Route Pattern: {routeEndpoint.RoutePattern}");
    }

    foreach (var endpointMetadata in currentEndpoint.Metadata)
    {
        Console.WriteLine($"  - Metadata: {endpointMetadata}");
    }

    await next(context);
});*/