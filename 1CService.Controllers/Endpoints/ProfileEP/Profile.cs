using _1CService.Application.Models.Profile.Request;
using _1CService.Application.UseCases.ProfileHandler.Command;
using _1CService.Application.UseCases.ProfileHandler.Queries;
using _1CService.Controllers.Models.Profile.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.ProfileEP
{
    public static class Profile
    {
        public static async Task<IResult> GetAppUserProfile(IGetProfileAppUser profileAppUser)
        {
            var profile = await profileAppUser.Get();

            return Results.Ok(profile);
        }
        public static async Task<IResult> SetAppUserProfile(ISetProfileAppUser setProfileAppUser, [FromBody] SetAppUserProfileDTO appUserProfileDTO)
        {
            var retUpdate = await setProfileAppUser.Set(new SetAppUserProfileQuery()
            {
                User1C = appUserProfileDTO.User1C,
                Password1C = appUserProfileDTO.Password1C,
            });

            return Results.Ok(retUpdate);
        }
    }
}
