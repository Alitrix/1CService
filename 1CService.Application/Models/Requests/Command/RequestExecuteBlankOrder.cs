using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Models.Requests.Command
{
    public struct RequestExecuteBlankOrder
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
    }
}
