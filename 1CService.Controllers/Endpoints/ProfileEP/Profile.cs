using _1CService.Application.DTO.Request;
using _1CService.Application.UseCases.Profile;
using _1CService.Controllers.ModelView;
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
        public static async Task<IResult> SetAppUserProfile(ISetProfileAppUser setProfileAppUser, [FromBody] RequestSetAppUserProfile appUserProfile)
        {
            var retUpdate = await setProfileAppUser.Set(new RequestSetAppUserProfileDTO()
            {
                User1C = appUserProfile.User1C,
                Password1C = appUserProfile.Password1C,
            });

            return Results.Ok(retUpdate);
        }
    }
}
