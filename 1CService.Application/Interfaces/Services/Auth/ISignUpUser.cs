using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface ISignUpUser
    {
        Task<AppUser> CreateUser(SignUpDTO user);
    }
}