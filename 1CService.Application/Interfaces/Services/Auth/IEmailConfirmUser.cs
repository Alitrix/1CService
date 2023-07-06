namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface IEmailConfirmUser
    {
        Task<bool> EmailTokenValidation(string userid, string token);
    }
}