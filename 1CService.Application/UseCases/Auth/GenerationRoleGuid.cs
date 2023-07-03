using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;

namespace _1CService.Application.UseCases.Auth
{
    public class GenerationRoleGuid : IGenerationRoleGuid
    {
        private readonly IRoleService _roleService;

        public GenerationRoleGuid(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<string> Generate(string userTypeAccess)
        {
            var guidRole = await _roleService.GenericGuidToRole(userTypeAccess);
            //New Generated and need send of Administration to Check Sms\WhatsUp\Email or other
            return guidRole.ToString();
        }
    }
}
