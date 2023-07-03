using _1CService.Application.Enums;

namespace _1CService.Application.Interfaces.Services
{
    public interface IGenerationRoleGuid
    {
        Task<string> Generate(string userTypeAccess);
    }
}