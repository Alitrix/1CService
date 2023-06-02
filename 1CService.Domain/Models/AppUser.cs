using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1CService.Domain.Enums;

namespace _1CService.Domain.Domain
{
    public class AppUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public int CountTry { get; set; }
        public bool Block { get; set; }
        public string User1C { get; set; }
        public string Password1C { get; set; }
        public string Email { get; set; } = string.Empty;
        public WorkPlace WorkPlace { get; set; } = WorkPlace.None;
    }
}
