using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.Interfaces.Services
{
    public interface IAuthenticateService
    {
        Task<AppUser?> GetCurrentUser();
        Task<IdentityResult> SignUp(SignInDTO signInDTO);
        Task<IdentityResult> SignIn(AuthDTO authDTO);
        Task<IdentityResult> SignOut(AppUser user);
    }
}