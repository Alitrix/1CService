using _1CService.Persistence.Repository;
using _1CService.Persistence.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using _1CService.Utilities;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Persistence
{
    public static class DependencyInjection
    {
        public static KeyManager keyManager_ = new KeyManager();

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<IAppUserDbContext, AppUserDbContext>();
            services.AddTransient<ISettings1CService, Settings1CService>();
            services.AddTransient<IService1C,Service1C>();
            services.AddTransient(typeof(IAsyncRepository<>), typeof(BlankOrderRepository<>));

            return services;
        }
    }
}
