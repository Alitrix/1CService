﻿using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Models;
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
            try
            {
                Database.EnsureCreated();
            }
            catch(Exception ex) 
            { 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
