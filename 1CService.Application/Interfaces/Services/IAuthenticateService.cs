using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.Interfaces.Services
{
    public interface IAuthenticateService
    {
        Task<AppUser> GetCurrentUser();
        Task<IdentityResult> SignIn(SignInDTO signInDTO);
        Task<IdentityResult> Login(AuthDTO authDTO);
        Task<IdentityResult> Logout(AppUser user);
    }
}