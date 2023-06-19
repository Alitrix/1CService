using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Persistence.Interfaces
{
    public interface IAuthenticateService
    {
        Task<AppUser> GetCurrentUser();
        Task<IdentityResult> TrySignIn(SignInDTO signInDTO);
        Task<IdentityResult> TryLogin(AuthDTO authDTO);
        Task<IdentityResult> TryLogOut(AppUser user);
    }
}