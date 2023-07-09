using _1CService.Application.Models;

namespace _1CService.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(AppUser user, string subject, string token);
    }
}