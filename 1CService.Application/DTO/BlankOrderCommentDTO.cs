﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO
{
    public struct BlankOrderCommentDTO
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
    }
}
