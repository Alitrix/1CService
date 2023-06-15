using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1CService.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.DTO
{
    public class AppUser : IdentityUser<string>
    {
        public string User1C { get; set; }
        public string Password1C { get; set; }
        public WorkPlace WorkPlace { get; set; }

        public override string ToString()
        {
            return UserName;
        }
    }
}
