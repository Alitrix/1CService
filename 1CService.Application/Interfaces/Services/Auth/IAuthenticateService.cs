using _1CService.Application.DTO;
using _1CService.Application.Models;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface IAuthenticateService
    {
        Task<PreRegistrationAppUserDTO?> SignUp(AppUser user, string password);
        Task<JwtAuthToken> SignIn(SignInDTO signInDTO);
        Task<SignOut> SignOut();
    }
}