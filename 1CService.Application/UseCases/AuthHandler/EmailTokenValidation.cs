using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class EmailTokenValidation : IEmailTokenValidation
    {
        IEmailService _emailService;
        public EmailTokenValidation(IEmailService emailService) =>
            _emailService = emailService;

        public async Task<bool> Validation(string userid, string token)
        {
            return await _emailService.Validation(userid, token);
        }
    }
}
