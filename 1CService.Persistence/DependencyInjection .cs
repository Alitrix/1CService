using _1CService.Application.Interfaces;
using _1CService.Persistence.Repository;
using _1CService.Persistence.Services;
using _1CService.Persistence.Interfaces;
using _1CService.Application.DTO;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace _1CService.Persistence
{
    public static class DependencyInjection
    {
        public static KeyManager keyManager_ = new KeyManager();

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<IAppUserDbContext, AppUserDbContext>();
            services.AddTransient(typeof(ISettings1CService), typeof(Settings1CService));
            services.AddTransient<IAuthenticateService, AuthenticationService>();
            services.AddTransient(typeof(IService1C), typeof(Service1C));
            services.AddTransient(typeof(IAsyncRepository<>), typeof(BlankOrderRepository<>));
            return services;
        }
    }
}
