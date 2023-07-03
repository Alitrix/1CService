using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using _1CService.Controllers.Endpoints;
using _1CService.Controllers.Endpoints.Auth;
using _1CService.Controllers.Endpoints.BlankOrdersEP;
using Microsoft.AspNetCore.Routing;
using _1CService.Application.Enums;

namespace _1CService.Controllers
{
    public static class DependencyInjection
    {
        public static WebApplication AddServiceEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => "Hello from Microservice");           
            app.MapGet("/test", TestPoint.Handler).RequireAuthorization(UserTypeAccess.User);


            //Auth
            app.MapPost("/oauth/sign-up", OAuth.SignUpHandler).AllowAnonymous();
            app.MapPost("/oauth/sign-in", OAuth.SignInHandler).AllowAnonymous();
            app.MapPost("/oauth/sign-out", OAuth.SignOutHandler).RequireAuthorization(UserTypeAccess.User);
            app.MapPost("/oauth/refresh-token", OAuth.RefreshTokenHandler).AllowAnonymous();
            app.MapPost("/oauth/role-add", OAuth.RoleAddHandler).RequireAuthorization(UserTypeAccess.User);
            app.MapGet("/oauth/req-add-manager", OAuth.GenerateAddRoleManager).RequireAuthorization(UserTypeAccess.User);

            //1C Service
            app.MapGet("/api/blankorders", BlankOrder.GetListBlankOrderHandler).RequireAuthorization(UserTypeAccess.Manager);
            app.MapPost("/api/blankorderdetail", BlankOrder.GetBlankOrderDetailHandler).RequireAuthorization(UserTypeAccess.Manager);

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