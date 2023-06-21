using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.UseCases.Auth
{
    public class SignUpUser : ISignUpUser
    {
        private readonly IAuthenticateService _authenticateService;

        public SignUpUser(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        public async Task<AppUser> Create(SignUpDTO user)
        {
            AppUser newUser = await _authenticateService.SignUp(user);
            return newUser;
        }
    }
}
