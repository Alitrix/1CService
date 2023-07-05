using _1CService.Application.DTO;
using _1CService.Application.Models.Auth.Response;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface IAuthenticateService
    {
        Task<AppUser> SignUp(AppUser user, string password);
        Task<JwtAuthToken> SignIn(SignInDTO signInDTO);
        Task<SignOut> SignOut();
    }
}