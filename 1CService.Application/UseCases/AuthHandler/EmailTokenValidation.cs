using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class EmailTokenValidation : IEmailTokenValidation
    {
        private readonly IEmailTokenService _emailTokenService;

        public EmailTokenValidation(IEmailTokenService emailTokenService)
        {
            _emailTokenService = emailTokenService;
        }
        public async Task<bool> Validation(string userid, string token)
        {
            return await _emailTokenService.ValidationEmailToken(userid, token).ConfigureAwait(false);
        }
    }
}
