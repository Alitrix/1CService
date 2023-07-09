using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class SignUpUser : ISignUpUser
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IEmailService _emailService;

        public SignUpUser(IAuthenticateService authenticateService, IEmailService emailService)
        {
            _authenticateService = authenticateService;
            _emailService = emailService;
        }
        public async Task<SignUp?> CreateUser(SignUpQuery signUpQuery)
        {
            var newUser = AppUser.CreateUser(signUpQuery.Email, signUpQuery.UserName);
            if (newUser == null)
                return null;

            AppUser? user = await _authenticateService.SignUp(newUser, signUpQuery.Password);
            if(user == null)
                return new SignUp()
                {
                    Message = "Error create user",
                };

            var originalCode = await _emailService.GenerateEmailConfirmationToken(newUser);
            //"Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме"
            await _emailService.SendEmailAsync(user, "Подтверждение регистрации", originalCode);
            return new SignUp()
            {
                Message = "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме",
                User = user.Id,
            };
        }
    }
}
