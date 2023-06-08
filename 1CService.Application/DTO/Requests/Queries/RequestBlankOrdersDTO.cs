using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO.Requests.Queries
{
    public struct RequestBlankOrdersDTO
    {
        public string WorkInPlace { get; set; }
        public RequestBlankOrdersDTO(string place) => WorkInPlace = place;
    }
}
