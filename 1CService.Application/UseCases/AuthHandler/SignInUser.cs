using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class SignInUser : ISignInUser
    {
        private readonly IAuthenticateService _authenticateService;

        public SignInUser(IAuthenticateService authenticateService) =>
            _authenticateService = authenticateService;

        public async Task<JwtAuthToken> Login(SignInQuery signInQuery)
        {
            JwtAuthToken token = await _authenticateService.SignIn(new SignInDTO()
            {
                Email = signInQuery.Email,
                Password = signInQuery.Password,
            });
            if (token.Access_Tokens.Equals(default))
                return new JwtAuthToken() { Error = "Error Sign" };
            return token;
        }
    }
}
