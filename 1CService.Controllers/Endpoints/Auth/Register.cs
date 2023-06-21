using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using _1CService.Persistence.Enums;
using _1CService.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Controllers.Endpoints.Auth
{
    public static class SignIn
    {
        public static async Task<IResult> Handler(KeyManager keyManager,
                HttpContext ctx,
                SignInManager<AppUser> signInManager,
                UserManager<AppUser> userManager,
                IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory, IAuthenticateService authService, [FromBody] AuthDTO auth)
        {
            var user = await userManager.FindByEmailAsync(auth.Email);
            if (user == null)
                return Results.BadRequest(new 
                {
                    Code = new UnauthorizedResult().StatusCode,
                    Message = "Authorization error",
                    Detail = $"User : {auth.Email} Invalid UserName or Password"
                });
            var result = await signInManager.CheckPasswordSignInAsync(user, auth.Password, false);
            if (result.Succeeded)
            {
                var principal = await claimsPrincipalFactory.CreateAsync(user);
                var identity = principal.Identities.First();
                identity.AddClaim(new Claim("amr", "pwd"));
                identity.AddClaim(new Claim(ClaimTypes.Role, UserTypeAccess.Operator.Name));

                var handle = new JsonWebTokenHandler();
                var key = new RsaSecurityKey(keyManager.RsaKey);
                var token = handle.CreateToken(new SecurityTokenDescriptor()
                {
                    Issuer = "https://localhost:7154",
                    Subject = identity,
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
                });

                return Results.Ok(new 
                {
                    Token = token  
                });
            }
            return Results.BadRequest(new 
            { 
                Code = new UnauthorizedResult().StatusCode,
                Message = "Authorization error",
                Detail = $"User : {auth.Email} Invalid UserName or Password"
            });
        }
    }
}
