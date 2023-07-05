using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface ISignInUser
    {
        Task<JwtAuthToken> Login(SignInQuery signInDTO);
    }
}