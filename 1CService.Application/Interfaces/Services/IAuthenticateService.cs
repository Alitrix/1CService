using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.Interfaces.Services
{
    public interface IAuthenticateService
    {
        Task<AppUser?> GetCurrentUser();
        Task<IdentityResult> SignUp(SignUpDTO signInDTO);
        Task<IdentityResult> SignIn(SignInDTO authDTO);
        Task<IdentityResult> SignOut(AppUser user);
    }
}