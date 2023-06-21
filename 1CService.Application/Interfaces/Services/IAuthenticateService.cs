using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.Interfaces.Services
{
    public interface IAuthenticateService
    {
        Task<AppUser?> GetCurrentUser();
        Task<AppUser> SignUp(SignUpDTO signUpDTO);
        Task<IdentityResult> SignIn(SignInDTO signInDTO);
        Task<IdentityResult> SignOut(AppUser user);
    }
}