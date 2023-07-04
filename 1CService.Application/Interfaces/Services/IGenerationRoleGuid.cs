using _1CService.Application.DTO.Response;
using _1CService.Application.Enums;

namespace _1CService.Application.Interfaces.Services
{
    public interface IGenerationRoleGuid
    {
        Task<ResponseGenerateRoleGuid> Generate(string userTypeAccess);
    }
}