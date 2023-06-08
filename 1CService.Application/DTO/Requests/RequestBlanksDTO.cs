using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO.Requests
{
    public struct RequestBlanksDTO
    {
        public string WorkInPlace { get; set; }
        public RequestBlanksDTO(string place) => this.WorkInPlace = place;
    }
}
