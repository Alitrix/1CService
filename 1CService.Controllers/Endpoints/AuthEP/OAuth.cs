using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;
using _1CService.Controllers.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.AuthEP
{
    public static class OAuth
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="signInDTO">Данные пользователя в формате JSON</param>
        /// <returns>JwtAuthToken</returns>
        public static async Task<IResult> SignInHandler(ISignInUser signInUser, [FromBody] SignInDTO signInDTO)
        {
            JwtAuthToken userTmp = await signInUser.Login(new SignInQuery()
            {
                Email = signInDTO.Email,
                Password = signInDTO.Password,
            });

            return Results.Ok(userTmp);
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="signUpDTO">Данные нового пользователя в формате JSON</param>
        /// <returns>SignUp</returns>
        public static async Task<IResult> SignUpHandler(ISignUpUser signUpUser, [FromBody] SignUpDTO signUpDTO)
        {
            SignUp? signUp = await signUpUser.CreateUser(new SignUpQuery()
            {
                Email = signUpDTO.Email,
                Password = signUpDTO.Password,
                UserName = signUpDTO.UserName,
            });

            //var code = HttpUtility.UrlEncode(signUp.Value.EmailConfirmation); // Need if this code send
            //var emailConfirm = linkgen.GetUriByName(ctx, "email-confirm", new { userid = signUp.Value.User, token = signUp.Value.EmailConfirmation });
            return signUp == null ? Results.NotFound(signUpDTO) : Results.Ok(signUp);
        }

        /// <summary>
        /// Выход авторизованного пользователя
        /// </summary>
        /// <returns>SignOut</returns>
        public static async Task<IResult> SignOutHandler(ISignOutUser signOutUser)
        {
            SignOut logOut = await signOutUser.Logout();

            return Results.Ok(logOut);
        }
    }
}
