using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO.Requests.Queries
{
    public struct RequestBlankOrderListDTO
    {
        public string WorkInPlace { get; set; }
        public RequestBlankOrderListDTO(string place) => WorkInPlace = place;
    }
}
