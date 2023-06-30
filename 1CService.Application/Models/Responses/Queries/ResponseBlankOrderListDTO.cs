using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1CService.Application.DTO;

namespace _1CService.Application.Models.Responses.Queries
{
    public struct ResponseBlankOrderListDTO
    {
        public IList<ListBlankOrderDTO> Documents { get; set; }
    }
}
