using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Profile.Request;
using _1CService.Controllers.Models.Profile.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.ProfileEP
{
    public static class Profile
    {
        public static async Task<IResult> GetProfile(IGetProfileAppUser profileAppUser)
        {
            var profile = await profileAppUser.Get();

            return Results.Ok(profile);
        }
        public static async Task<IResult> SetProfile(ISetProfileAppUser setProfileAppUser, [FromBody] SetAppUserProfileDTO appUserProfileDTO)
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
