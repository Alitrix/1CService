using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using _1CService.Domain.Enums;
using _1CService.Persistence.Enums;
using _1CService.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Persistence.Services.FirstStart
{
    public static class InititalizeDB
    {
        public static async Task AddUserAdmin(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var usrMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var user = new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    Email = "admin@admin.ru",
                    SecurityStamp = RndGenerator.GenerateSecurityStamp(),
                    User1C = "Alitrix",
                    WorkPlace = WorkPlace.None,
                    Password1C = "None",
                };
                if (await usrMgr.FindByEmailAsync(user.Email) == null)
                {
                    IdentityResult createUserResult = await usrMgr.CreateAsync(user, "Pa$$w0rd").ConfigureAwait(false);
                    if (createUserResult.Succeeded)
                    {
                        createUserResult = await usrMgr.AddClaimAsync(user, new Claim(ClaimTypes.Role, UserTypeAccess.Administrator.Name)).ConfigureAwait(false);
                    }
                }
            }
        }
    }
}
