using _1CService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public bool IsWork => BlankOrder.BlankStatus.Equals(ExecuteBlankOrderType.Work.ToString());

        public static ExecuteBlankOrder CreateEmptyBlank()
        {
            return new ExecuteBlankOrder
            {
                Id = Guid.NewGuid(),
                User = new AppUser(),
                BlankOrder = new BlankOrder(),
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                ExecuteStatus = ExecuteBlankOrderType.None
            };
        }
        public static ExecuteBlankOrder CreateBlankExecute(AppUser user, BlankOrder blankOrder, ExecuteBlankOrderType executeStatus)
        {
            return new ExecuteBlankOrder()
            {
                Id = Guid.NewGuid(),
                User = user,
                BlankOrder = blankOrder,
                ExecuteStatus = executeStatus,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
        }
    }
}
