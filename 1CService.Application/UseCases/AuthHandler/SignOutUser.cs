using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class SignOutUser : ISignOutUser
    {
        private readonly IAuthenticateService _authenticateService;

        public SignOutUser(IAuthenticateService authenticateService) => 
            _authenticateService = authenticateService;

        public async Task<SignOut> Logout()
        {
            SignOut signOutResult = await _authenticateService.SignOut();
            return signOutResult;
        }
    }
}
