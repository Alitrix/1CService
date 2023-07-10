using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.UseCases.AuthHandler
{
    public class RequestAddRights : IRequestAddRights
    {
        private readonly IRoleService _roleService;
        private readonly IEmailService _emailService;
        private readonly IAppUserService _appUserService;

        public RequestAddRights(IRoleService roleService, IEmailService emailService, IAppUserService appUserService) =>
            (_roleService, _emailService, _appUserService) = (roleService, emailService, appUserService);

        public async Task<ResponseMessage> Generate(string userTypeAccess)
        {
            var guidRole = await _roleService.GenerateGuidFromRole(userTypeAccess);
            if(guidRole.Equals(Guid.Empty))
                return new ResponseMessage()
                {
                    Error = "Error sent request",
                    Success = false
                };
            
            var currentUser = await _appUserService.GetCurrentUser();
            if (currentUser == null) 
                return new ResponseMessage() 
                {
                    Success = false,
                    Error = "No Auth user"
                };
            var sendMail = await _emailService.SendEmailRequestUpgradeRights(currentUser, 
                                $"Поступил запрос на повышение прав от :{currentUser}", guidRole.ToString()).ConfigureAwait(false);

            return new ResponseMessage()
            {
                Message = sendMail,
                Error = "",
                Success = true
            };
        }
    }
}
