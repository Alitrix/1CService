using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO
{
    public class ListBlankOrderDTO
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Manager { get; set; }
        public string Contragent { get; set; }
        public int Urgency { get; set; }
        public string CompletionDate { get; set; }
        public string ExecuteState { get; set; }
    }
}
