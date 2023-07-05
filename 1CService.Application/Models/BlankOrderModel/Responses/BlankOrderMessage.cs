using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Models.BlankOrderModel.Responses
{
    public struct BlankOrderMessage
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
