using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.UseCases.RoleHandler
{
    public class RequestAddRights : IRequestAddRights
    {
        private readonly IRoleService _roleService;
        private readonly IEmailService _emailService;
        private readonly IAppUserService _appUserService;
        private readonly ITokenService _tokenService;
        private readonly IRedisService _redisService;
        private readonly RedisConfiguration _redisConfiguration;

        public RequestAddRights(IRoleService roleService, IEmailService emailService,
                                IAppUserService appUserService, ITokenService tokenService, 
                                IRedisService redisService, RedisConfiguration redisConfiguration) =>
            (_roleService, _emailService, _appUserService, _tokenService, _redisService, _redisConfiguration) =
            (roleService, emailService, appUserService, tokenService, redisService, redisConfiguration);

        public async Task<ResponseMessage> Generate(string requestTypeAccess)
        {
            var currentUser = await _appUserService.GetCurrentUser();
            if (currentUser == null)
                return new ResponseMessage()
                {
                    Success = false,
                    Error = "No Auth user"
                };

            UserRoleRequestItem? genRequestAddRight = await _roleService.GenerateGuidFromRoleForUser(requestTypeAccess, currentUser).ConfigureAwait(false);
            if (genRequestAddRight == null)
                return new ResponseMessage()
                {
                    Error = "Error sent request",
                    Success = false
                };

            var token = _tokenService.GenerateShortToken();

            if (!_redisService.Set(token, genRequestAddRight, _redisConfiguration.GetTimeRequestAddRight()))
                return new ResponseMessage()
                {
                    Error = "Error write Request to Redis",
                    Success = false
                };

            var sendMail = await _emailService.SendEmailRequestUpgradeRights(currentUser,
                                $"Поступил запрос на повышение прав от :{currentUser}", token).ConfigureAwait(false);

            return new ResponseMessage()
            {
                Message = $"{DateTime.Now}::A request to add rights has been sent to the company administrator. ({sendMail})",
                Error = "",
                Success = true
            };
        }
    }
}
