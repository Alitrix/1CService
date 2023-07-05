using _1CService.Application.Enums;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.Interfaces.Services
{
    public interface IGenerateRoleGuid
    {
        Task<GenerateGuidMessage> Generate(string userTypeAccess);
    }
}