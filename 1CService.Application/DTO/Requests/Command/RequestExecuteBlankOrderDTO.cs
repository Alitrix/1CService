﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO.Requests.Command
{
    public struct RequestExecuteBlankOrderDTO
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
        public string Msg { get; set; }
    }
}