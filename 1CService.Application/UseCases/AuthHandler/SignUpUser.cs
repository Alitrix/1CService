using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models;
using _1CService.Application.Models.Auth.Request;

namespace _1CService.Application.UseCases.Auth
{
    public class SignUpUser : ISignUpUser
    {
        private readonly IAuthenticateService _authenticateService;

        public SignUpUser(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        public async Task<AppUser> CreateUser(SignUpQuery signUpDto)
        {
            var newUser = AppUser.CreateUser(signUpDto.Email, signUpDto.UserName);
            AppUser user = await _authenticateService.SignUp(newUser, signUpDto.Password);
            return user;
        }
    }
}
