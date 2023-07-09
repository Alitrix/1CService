using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO
{
    public class EmailConfiguration
    {
        public required string From { get; set; }
        public required string SmtpServer { get; set; }
        public int Port { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string NotificationMail { get; set; }
    }
}
