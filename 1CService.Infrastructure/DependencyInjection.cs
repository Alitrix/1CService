using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Infrastructure.Services;
using _1CService.Utilities;
using _1CService.Application.Interfaces.Repositories;

namespace _1CService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<KeyManager, KeyManager>();
            services.AddSingleton<AppTokenValidationParameters, AppTokenValidationParameters>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IService1C, Service1C>();
            services.AddTransient<IAuthenticateService, AuthenticationService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailTokenService, EmailTokenService>();
            services.AddTransient<IProfileService,  ProfileService>();
            services.AddTransient<IRedisService, RedisService>();


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                {
                    //o.IncludeErrorDetails = true;
                    o.RequireHttpsMetadata = false; // true
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("TOKEN_EXPIRED", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };

                    o.Configuration = new OpenIdConnectConfiguration()
                    {
                        SigningKeys =
                        {
                            new RsaSecurityKey(new KeyManager().RsaKey)
                        },
                    };
                    o.MapInboundClaims = false;
                });
            return services;
        }
    }
}
