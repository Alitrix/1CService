using _1CService.Application.Models;

namespace _1CService.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task<string> GenerateEmailConfirmationToken(AppUser? user = null);
        Task SendEmailAsync(AppUser user, string subject, string token);
        Task<bool> Validation(string userid, string token);
    }
}