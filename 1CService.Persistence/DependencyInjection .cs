using _1CService.Persistence.Repository;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using _1CService.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using _1CService.Application.Models;

namespace _1CService.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            //builder.Services.AddDbContext<AppUserDbContext>(c => c.UseInMemoryDatabase("my_db"));


            services.AddDbContext<AppUserDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AppUserDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager();


            services.AddTransient<IAppUserDbContext, AppUserDbContext>();
            services.AddTransient(typeof(IBlankOrderRepository), typeof(BlankOrderRepository));
            return services;
        }
    }
}
