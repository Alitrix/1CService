using _1CService.Application.Enums;
using _1CService.Application.Interfaces;
using _1CService.Application.Models;
using _1CService.Domain.Enums;
using _1CService.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Persistence.Repository
{
    public static class DbInitializer
    {
        public static async Task Initialize(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var usrMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                
                if (await roleMgr.FindByNameAsync(UserTypeAccess.User) == null)
                {
                    var roleUser = new IdentityRole(UserTypeAccess.User) { ConcurrencyStamp = RndGenerator.GenerateSecurityStamp() };
                    if(await roleMgr.CreateAsync(roleUser) == IdentityResult.Success)
                        await roleMgr.AddClaimAsync(roleUser, new Claim(UserTypeAccess.User, "User"));
                }
                if (await roleMgr.FindByNameAsync(UserTypeAccess.Manager) == null)
                {
                    var roleManager = new IdentityRole(UserTypeAccess.Manager) { ConcurrencyStamp = RndGenerator.GenerateSecurityStamp() };
                    if (await roleMgr.CreateAsync(roleManager) == IdentityResult.Success)
                        await roleMgr.AddClaimAsync(roleManager, new Claim(UserTypeAccess.Manager, "Manager"));
                }
                if (await roleMgr.FindByNameAsync(UserTypeAccess.Director) == null)
                {
                    var roleDirector = new IdentityRole(UserTypeAccess.Director) { ConcurrencyStamp = RndGenerator.GenerateSecurityStamp() };
                    if (await roleMgr.CreateAsync(roleDirector) == IdentityResult.Success)
                        await roleMgr.AddClaimAsync(roleDirector, new Claim(UserTypeAccess.Director, "Director"));
                }
                if (await roleMgr.FindByNameAsync(UserTypeAccess.Administrator) == null)
                {
                    var roleAdministrator = new IdentityRole(UserTypeAccess.Administrator) { ConcurrencyStamp = RndGenerator.GenerateSecurityStamp() };
                    if (await roleMgr.CreateAsync(roleAdministrator) == IdentityResult.Success)
                        await roleMgr.AddClaimAsync(roleAdministrator, new Claim(UserTypeAccess.Administrator, "Administrator"));
                }


                var user = new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    Email = "admin@admin.ru",
                    SecurityStamp = RndGenerator.GenerateSecurityStamp(),
                    User1C = "Alitrix",
                    WorkPlace = WorkPlace.None,
                    Password1C = "None",
                    ServiceAddress = "srv",
                    ServiceSection = "MobileService",
                    ServiceBaseName = "smyk",
                };

                if (await usrMgr.FindByEmailAsync(user.Email) == null)
                {
                    IdentityResult createUserResult = await usrMgr.CreateAsync(user, "Pa$$w0rd").ConfigureAwait(false);
                    if (createUserResult.Succeeded)
                    {
                        var identityRole = await roleMgr.FindByNameAsync(UserTypeAccess.Administrator);
                        if (identityRole != null)
                        {
                            if(!string.IsNullOrEmpty( identityRole.Name))
                                await usrMgr.AddToRoleAsync(user, identityRole.Name);
                        }
                    }
                }
            }
        }
    }
}
