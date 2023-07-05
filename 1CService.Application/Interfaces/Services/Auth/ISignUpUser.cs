using _1CService.Application.DTO;
using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface ISignUpUser
    {
        Task<AppUser> CreateUser(SignUpQuery user);
    }
}