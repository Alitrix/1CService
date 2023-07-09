using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.Interfaces.UseCases
{
    public interface IRequestAddRights
    {
        Task<ResponseMessage> Generate(string userTypeAccess);
    }
}