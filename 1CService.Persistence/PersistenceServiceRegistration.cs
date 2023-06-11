using _1CService.Application.Interfaces;
using _1CService.Persistence.Repository;
using _1CService.Persistence.API;
using Microsoft.Extensions.DependencyInjection;

namespace _1CService.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IService1C), typeof(Service1C));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BlankOrderRepository<>));
            return services;
        }
    }
}
