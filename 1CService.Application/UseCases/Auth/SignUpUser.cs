using _1CService.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.UseCases.Auth
{
    public class SignUpUser
    {
        private readonly IAuthenticateService _authenticateService;

        public SignUpUser(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
    }
}
