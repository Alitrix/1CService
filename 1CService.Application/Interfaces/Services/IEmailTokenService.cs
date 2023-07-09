using _1CService.Application.Models;

namespace _1CService.Application.Interfaces.Services
{
    public interface IEmailTokenService
    {
        Task<string> GenerateEmailConfirmationToken(AppUser? user = null);
        Task<bool> ValidationEmailToken(string userid, string token);
    }
}