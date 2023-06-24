using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO
{
    public class JwtTokenDTO
    {
        public string Token { get; set; }
        public long TimeExp { get; set; }
        public string Error { get; set; }
    }
}
