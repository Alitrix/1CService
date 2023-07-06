using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using _1CService.Controllers.Endpoints.AuthEP;
using _1CService.Controllers.Endpoints.BlankOrdersEP;
using Microsoft.AspNetCore.Routing;
using _1CService.Application.Enums;
using _1CService.Controllers.Endpoints.ProfileEP;
using _1CService.Controllers.Endpoints.TokenEP;
using _1CService.Controllers.Endpoints.RoleEP;

namespace _1CService.Controllers
{
    public static class DependencyInjection
    {
        public static WebApplication AddServiceEndpoints(this WebApplication app)
        {
            //Welcome Part:)
            app.MapGet("/", () => "Hello from ASP.NET Core Microservice");   
            
            //Auth
            app.MapPost("/oauth/sign-up", OAuth.SignUpHandler).AllowAnonymous();
            app.MapPost("/oauth/sign-in", OAuth.SignInHandler).AllowAnonymous();
            app.MapGet("/oauth/sign-out", OAuth.SignOutHandler).RequireAuthorization(UserTypeAccess.User);

            //Token
            app.MapPost("/token/refresh-token", Token.RefreshTokenHandler).AllowAnonymous();

            //Role
            app.MapPost("/role/add-role", Role.RoleAddHandler).RequireAuthorization(UserTypeAccess.User);
            app.MapGet("/role/request-role-manager", Role.RequestAddRoleManagerHandler).RequireAuthorization(UserTypeAccess.User);

            //Profile`s AppUser
            app.MapGet("/profile/user", Profile.GetAppUserProfile).RequireAuthorization(UserTypeAccess.Manager);
            app.MapPost("/profile/user", Profile.SetAppUserProfile).RequireAuthorization(UserTypeAccess.Manager);


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