using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace _1CService.Application.Interfaces.Services.Auth
{
    public interface IAuthenticateService
    {
        Task<AppUser> SignUp(AppUser user, string password);
        Task<JwtTokenDTO> SignIn(SignInDTO signInDTO);
        Task<SignOutDto> SignOut();
        Task<JwtTokenDTO> RefreshToken(RefreshTokensDTO refreshTokens);
    }
}