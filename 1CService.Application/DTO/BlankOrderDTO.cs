using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.DTO
{
    public struct BlankOrderDTO //need restructure the list of fields
    {
        public string Number;
        public string Date;
        public string Manager;
        public string Contragent;
        public int Urgency;
        public string CompletionDate;
        public string ExecuteState;
    }
}
