using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Services
{
    public interface ISignUpUser
    {
        Task<AppUser> CreateUser(SignUpDTO user);
    }
}