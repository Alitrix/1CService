using _1CService.Application.DTO;

namespace _1CService.Application.UseCases.Auth
{
    public interface ISignUpUser
    {
        Task<AppUser> Create(SignUpDTO user);
    }
}