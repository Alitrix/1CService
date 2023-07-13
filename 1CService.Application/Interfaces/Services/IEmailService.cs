using _1CService.Application.DTO;
using _1CService.Application.Models;

namespace _1CService.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task<string> SendEmailRequestUpgradeRights(AppUser from_user, string subject, string token);
        Task<string> SendEmailConfirmTokenAsync(AppUser user, string subject, string token);
        Task<string> SendEmailToAsync(string to_email, string subject, string message_text);
    }
}