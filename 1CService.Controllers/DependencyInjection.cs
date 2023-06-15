using _1CService.Application.DTO;
using _1CService.Persistence.Enums;
using _1CService.Persistence.Services;
using _1CService.WebApi.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace _1CService.Controllers
{
    public static class DependencyInjection
    {
        public static WebApplication AddEndpoints(this WebApplication app)
        {
            app.MapGet("/Login", GetLogin.Handler).AllowAnonymous();
            app.MapGet("/", (ClaimsPrincipal user) => user.Claims.Select(x => KeyValuePair.Create(x.Type, x.Value))).RequireAuthorization(UserTypeAccess.Operator.Name, "amr");
            app.MapGet("/secret", () => "secret");


            app.MapGet("/sign-in", async (KeyManager keyManager,
                HttpContext ctx,
                SignInManager<AppUser> signInManager,
                UserManager<AppUser> userManager,
                IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory, [FromBody] AuthDTO auth) =>
            {
                var user = await userManager.FindByEmailAsync(auth.Email);
                if (user == null)
                    return "Auth Failed !!!";
                var result = await signInManager.CheckPasswordSignInAsync(user, auth.Password, false);
                if (result.Succeeded)
                {
                    var principal = await claimsPrincipalFactory.CreateAsync(user);
                    var identity = principal.Identities.First();
                    identity.AddClaim(new Claim("method", "jwt"));
                    identity.AddClaim(new Claim("amr", "pwd"));
                    identity.AddClaim(new Claim(ClaimTypes.Role, UserTypeAccess.Operator.Name));

                    var handle = new JsonWebTokenHandler();
                    var key = new RsaSecurityKey(keyManager.RsaKey);
                    var token = handle.CreateToken(new SecurityTokenDescriptor()
                    {
                        Issuer = "https://localhost:7154",
                        Subject = identity,
                        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
                    });

                    return token;
                }
                return "Auth Failed !!!";

            });

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