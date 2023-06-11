using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Models.Requests.Queries
{
    public struct RequestBlankOrderList
    {
        public string WorkInPlace { get; set; }
        public RequestBlankOrderList(string place) => WorkInPlace = place;
    }
}
