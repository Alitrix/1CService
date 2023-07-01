using _1CService.Persistence.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using _1CService.Controllers.Endpoints;
using _1CService.Controllers.Endpoints.Auth;
using _1CService.Controllers.Endpoints.BlankOrdersEP;

namespace _1CService.Controllers
{
    public static class DependencyInjection
    {
        public static WebApplication AddServiceEndpoints(this WebApplication app)
        {

            app.MapGet("/info", () => "This REST service works with the mobile version of Smyk.Mobile.App v1.0.1");           
            app.MapGet("/test", TestPoint.Handler).RequireAuthorization(UserTypeAccess.User);


            //Authentication and Autorization
            app.MapPost("/oauth/sign-up", OAuth.SignUpHandler).AllowAnonymous();
            app.MapPost("/oauth/sign-in", OAuth.SignInHandler).AllowAnonymous();
            app.MapPost("/oauth/sign-out", OAuth.SignOutHandler).RequireAuthorization(UserTypeAccess.User);
            app.MapPost("/oauth/refresh-token", OAuth.RefreshTokenHandler).AllowAnonymous();


            //1C Use Cases
            app.MapPost("/api/blankorders", BlankOrders.GetListBlankOrderHandler).RequireAuthorization(UserTypeAccess.Manager);

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