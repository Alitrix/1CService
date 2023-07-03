using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface ISignInUser
    {
        Task<JwtTokenDTO> Login(SignInDTO signInDTO);
    }
}