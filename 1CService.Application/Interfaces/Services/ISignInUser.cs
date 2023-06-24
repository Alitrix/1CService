using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Services
{
    public interface ISignInUser
    {
        Task<JwtTokenDTO> Login(SignInDTO signInDTO);
    }
}