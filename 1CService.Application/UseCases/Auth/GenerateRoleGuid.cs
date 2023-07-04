using _1CService.Application.DTO.Response;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;

namespace _1CService.Application.UseCases.Auth
{
    public class GenerateRoleGuid : IGenerationRoleGuid
    {
        private readonly IRoleService _roleService;

        public GenerateRoleGuid(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<ResponseGenerateRoleGuid> Generate(string userTypeAccess)
        {
            var guidRole = await _roleService.GenericGuidToRole(userTypeAccess);
            //New Generated and need send of Administration to Check Sms\WhatsUp\Email or other
            if(guidRole.Equals(Guid.Empty))
                return new ResponseGenerateRoleGuid()
                {
                    Error = "Error sent request",
                    Success = true
                };

            return new ResponseGenerateRoleGuid()
            {
                Message = $"A request to upgrade rights has been sent Administrator.{ guidRole }",
                Error = "",
                Success = true
            };
        }
    }
}
