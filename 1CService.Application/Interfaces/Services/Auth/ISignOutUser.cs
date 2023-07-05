using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface ISignOutUser
    {
        Task<SignOut> Logout();
    }
}