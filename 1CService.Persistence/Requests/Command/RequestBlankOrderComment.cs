using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO.Requests.Command
{
    public struct RequestBlankOrderComment
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }
    }
}
