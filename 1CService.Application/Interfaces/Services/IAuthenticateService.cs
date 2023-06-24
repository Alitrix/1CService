using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.Interfaces.Services
{
    public interface IAuthenticateService
    {
        Task<AppUser?> GetCurrentUser();
        Task<AppUser> SignUp(AppUser user, string password);
        Task<JwtTokenDTO> SignIn(SignInDTO signInDTO);
        Task<IdentityResult> SignOut(AppUser user);
    }
}