using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.Interfaces.UseCases
{
    public interface ISignOutUser
    {
        Task<SignOut> Logout();
    }
}