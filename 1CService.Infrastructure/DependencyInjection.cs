using _1CService.Application.Interfaces.Services;
using _1CService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _1CService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticateService, AuthenticationService>();
            return services;
        }
    }
}
