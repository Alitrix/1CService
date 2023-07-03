using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface ISignOutUser
    {
        Task<SignOutDto> Logout();
    }
}