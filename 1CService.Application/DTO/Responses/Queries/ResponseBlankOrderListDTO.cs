using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO.Responses.Queries
{
    public struct ResponseBlankOrderListDTO
    {
        public IList<BlankOrderDTO> BlankOrders { get; set; }
    }
}
