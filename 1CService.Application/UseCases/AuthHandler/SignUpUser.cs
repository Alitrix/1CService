using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.UseCases.Auth
{
    public class SignUpUser : ISignUpUser
    {
        private readonly IAuthenticateService _authenticateService;
        public SignUpUser(IAuthenticateService authenticateService) => 
            _authenticateService = authenticateService;

        public async Task<SignUp?> CreateUser(SignUpQuery signUpQuery)
        {
            var newUser = AppUser.CreateUser(signUpQuery.Email, signUpQuery.UserName);
            AppUser? user = await _authenticateService.SignUp(newUser, signUpQuery.Password);
            if(user == null)
                return new SignUp()
                {
                    Message = "Error create user",
                };

            return new SignUp()
            {
                Message = "Created User",
                User = user,
            };
        }
    }
}
