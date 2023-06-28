using _1CService.Application.DTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace _1CService.Application.Interfaces.Services
{
    public interface IAuthenticateService
    {
        Task<AppUser?> GetCurrentUser();
        Task<IList<Claim>> GetCurrentClaims();
        Task<AppUser> SignUp(AppUser user, string password);
        Task<JwtTokenDTO> SignIn(SignInDTO signInDTO);
        Task<IdentityResult> SignOut(AppUser user);
        Task<JwtTokenDTO> RefreshToken(RefreshTokensDTO refreshTokens);
    }
}