using _1CService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Domain.Domain
{
    public class ExecuteBlankOrder
    {
        public Guid Id { get; set; }
        public AppUser User { get; set; }
        public BlankOrder BlankOrder { get; set; }
        public ExecuteBlankOrderType ExecuteStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
