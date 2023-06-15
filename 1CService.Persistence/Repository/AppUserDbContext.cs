using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using _1CService.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _1CService.Persistence.Repository
{
    public class AppUserDbContext : IdentityDbContext<AppUser, IdentityRole, string>, IAppUserDbContext
    {
        public AppUserDbContext(DbContextOptions<AppUserDbContext> options)
           : base(options) 
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
