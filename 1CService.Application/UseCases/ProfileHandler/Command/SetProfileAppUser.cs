﻿using _1CService.Application.Interfaces.Services;
using _1CService.Application.Models;
using _1CService.Application.Models.Profile.Request;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.UseCases.ProfileHandler.Command
{
    public class SetProfileAppUser : ISetProfileAppUser
    {
        private readonly IAppUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public SetProfileAppUser(IAppUserService userService, UserManager<AppUser> userManager) => 
            (_userService, _userManager) = (userService, userManager);

        public async Task<bool> Set(SetAppUserProfileQuery request)
        {
            var currentUser = await _userService.GetCurrentUser();
            if (currentUser == null)
                return false;

            currentUser.User1C = request.User1C;
            currentUser.Password1C = request.Password1C;

            var retUpdate = await _userManager.UpdateAsync(currentUser);
            if (!retUpdate.Succeeded)
                return false;

            return true;
        }
    }
}
