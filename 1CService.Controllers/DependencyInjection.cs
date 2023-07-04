using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using _1CService.Controllers.Endpoints;
using _1CService.Controllers.Endpoints.AuthEP;
using _1CService.Controllers.Endpoints.BlankOrdersEP;
using Microsoft.AspNetCore.Routing;
using _1CService.Application.Enums;
using _1CService.Controllers.Endpoints.ProfileEP;

namespace _1CService.Controllers
{
    public static class DependencyInjection
    {
        public static WebApplication AddServiceEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => "Hello from ASP.NET Core Microservice");           

            //Auth
            app.MapPost("/oauth/sign-up", OAuth.SignUpHandler).AllowAnonymous();
            app.MapPost("/oauth/sign-in", OAuth.SignInHandler).AllowAnonymous();
            app.MapPost("/oauth/sign-out", OAuth.SignOutHandler).RequireAuthorization(UserTypeAccess.User);
            app.MapPost("/oauth/refresh-token", OAuth.RefreshTokenHandler).AllowAnonymous();
            app.MapPost("/oauth/add-role", OAuth.RoleAddHandler).RequireAuthorization(UserTypeAccess.User);
            app.MapGet("/oauth/request-role", OAuth.RequestAddRoleManagerHandler).RequireAuthorization(UserTypeAccess.User);

            //Profile`s AppUser and Service
            app.MapPost("/profile/get", Profile.GetAppUserProfile).RequireAuthorization(UserTypeAccess.Manager);
            app.MapPost("/profile/set", Profile.SetAppUserProfile).RequireAuthorization(UserTypeAccess.Manager);

            //1C Service
            app.MapGet("/blankorder/list", BlankOrder.GetListBlankOrderHandler).RequireAuthorization(UserTypeAccess.Manager);
            app.MapPost("/blankorder/detail", BlankOrder.GetBlankOrderDetailHandler).RequireAuthorization(UserTypeAccess.Manager);

            app.MapPost("/blankorder/inwork", BlankOrder.AcceptInWorkBlankOrderHandler).RequireAuthorization(UserTypeAccess.Manager);
            app.MapPost("/blankorder/add-comment", BlankOrder.AddCommentToBlankOrderHandler).RequireAuthorization(UserTypeAccess.Manager);

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