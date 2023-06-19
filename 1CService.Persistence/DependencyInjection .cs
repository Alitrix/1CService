using _1CService.Application.Interfaces;
using _1CService.Persistence.Repository;
using _1CService.Persistence.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using _1CService.Utilities;
using _1CService.Infrastructure.Interfaces;

namespace _1CService.Persistence
{
    public static class DependencyInjection
    {
        public static KeyManager keyManager_ = new KeyManager();

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<IAppUserDbContext, AppUserDbContext>();
            //services.AddTransient<IAuthenticateService, AuthenticationService>();
            services.AddTransient(typeof(ISettings1CService), typeof(Settings1CService));
            services.AddTransient(typeof(IService1C), typeof(Service1C));
            services.AddTransient(typeof(IAsyncRepository<>), typeof(BlankOrderRepository<>));
            return services;
        }
    }
}
