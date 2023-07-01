using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Application.UseCases.Auth
{
    public class SignInUser : ISignInUser
    {
        private readonly IAuthenticateService _authenticateService;

        public SignInUser(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        public async Task<JwtTokenDTO> Login(SignInDTO signInDTO)
        {
            JwtTokenDTO token = await _authenticateService.SignIn(signInDTO);
            if (token.Access_Tokens == null)
                return await Task.FromResult(new JwtTokenDTO() { Error = "Erro Sign" });
            return token;
        }
    }
}
