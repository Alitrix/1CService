using _1CService.Application.Models;
using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface ISignUpUser
    {
        Task<AppUser> CreateUser(SignUpQuery user);
    }
}