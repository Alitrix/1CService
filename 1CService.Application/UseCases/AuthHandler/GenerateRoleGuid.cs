using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class GenerateRoleGuid : IGenerateRoleGuid
    {
        private readonly IRoleService _roleService;

        public GenerateRoleGuid(IRoleService roleService) => _roleService = roleService;

        public async Task<ResponseMessage> Generate(string userTypeAccess)
        {
            var guidRole = await _roleService.GenericGuidToRole(userTypeAccess);

            //New Generated and need send of Administration to Check Sms\WhatsUp\Email or other
            if(guidRole.Equals(Guid.Empty))
                return new ResponseMessage()
                {
                    Error = "Error sent request",
                    Success = false
                };

            return new ResponseMessage()
            {
                Message = $"A request to upgrade rights has been sent Administrator.{ guidRole }",
                Error = "",
                Success = true
            };
        }
    }
}
