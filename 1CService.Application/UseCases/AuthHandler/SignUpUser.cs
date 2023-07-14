using _1CService.Application.DTO;
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
            var newUser = AppUser.Create(signUpQuery.Email, signUpQuery.UserName);
            if (newUser == null)
                return null;

            PreRegistrationAppUserDTO? preUser = await _authenticateService.SignUp(newUser, signUpQuery.Password).ConfigureAwait(false);
            if(preUser == null)
                return new SignUp()
                {
                    Message = "Error create user",
                };

            await _emailService.SendEmailConfirmTokenAsync(preUser.User, "Подтверждение регистрации", preUser.EmailTokenConfirm).ConfigureAwait(false);
            return new SignUp()
            {
                Message = "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме",
                User = preUser.User.Id,
            };
        }
    }
}
