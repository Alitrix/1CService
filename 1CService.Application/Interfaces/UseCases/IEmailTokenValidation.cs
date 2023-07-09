namespace _1CService.Application.Interfaces.UseCases
{
    public interface IEmailTokenValidation
    {
        Task<bool> Validation(string userid, string token);
    }
}