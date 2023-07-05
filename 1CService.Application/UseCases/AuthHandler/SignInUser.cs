using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.UseCases.Auth
{
    public class SignInUser : ISignInUser
    {
        private readonly IAuthenticateService _authenticateService;

        public SignInUser(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        public async Task<JwtAuthToken> Login(SignInQuery signInQuery)
        {
            JwtAuthToken token = await _authenticateService.SignIn(signInQuery);
            if (token.Access_Tokens == null)
                return await Task.FromResult(new JwtAuthToken() { Error = "Error Sign" });
            return token;
        }
    }
}
