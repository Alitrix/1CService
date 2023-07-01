using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Application.UseCases.Auth
{
    public class SignOutUser : ISignOutUser
    {
        private readonly IAuthenticateService _authenticateService;

        public SignOutUser(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        public async Task<SignOutDto> Logout()
        {
            SignOutDto signOutResult = await _authenticateService.SignOut();
            return signOutResult;
        }
    }
}
