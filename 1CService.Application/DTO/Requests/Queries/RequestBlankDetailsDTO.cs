﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO.Requests.Queries
{
    public struct RequestBlankDetailsDTO
    {
        public string Number { get; set; }

        public string Date { get; set; }

        public RequestBlankDetailsDTO(string number, string date)
        {
            Number = number;
            Date = date;
        }
    }
}