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
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public WorkPlace WorkPlace { get; set; } = WorkPlace.None;
        public string UserName { get; set; } = string.Empty;
    }
}
