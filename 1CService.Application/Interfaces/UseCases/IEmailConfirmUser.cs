namespace _1CService.Application.Interfaces.UseCases
{
    public interface IEmailConfirmUser
    {
        Task<bool> Validation(string userid, string token);
    }
}