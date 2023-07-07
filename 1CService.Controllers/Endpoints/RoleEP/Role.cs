using _1CService.Application.Enums;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;
using Microsoft.AspNetCore.Http;
using _1CService.Application.Interfaces.UseCases;

namespace _1CService.Controllers.Endpoints.RoleEP
{
    public static class Role
    {
        public static async Task<IResult> RoleAddHandler(IRoleAddToUser roleAddToUser, string guid)
        {
            AddRoleResponse addRoleToUser = await roleAddToUser.AddRole(guid);
            return Results.Ok(addRoleToUser);
        }
        public static async Task<string> RequestAddRoleManagerHandler(IGenerateRoleGuid requestGenerationRole)
        {
            ResponseMessage response = await requestGenerationRole.Generate(UserTypeAccess.Manager);
            return response.Message;
        }
    }
}
