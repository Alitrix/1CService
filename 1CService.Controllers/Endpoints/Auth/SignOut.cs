using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using _1CService.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Controllers.Endpoints.Auth
{
    public static class SignOut
    {
        public static async Task<IResult> Handler(KeyManager keyManager,
               HttpContext ctx,
               SignInManager<AppUser> signInManager,
               UserManager<AppUser> userManager,
               IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory, IAuthenticateService authService, [FromBody] SignInDTO auth)
        {
            return Results.Empty;
        }
    }
}
