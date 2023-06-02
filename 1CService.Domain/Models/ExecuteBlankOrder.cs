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

        public ExecuteBlankOrder()
        {
            Updated = Created = DateTime.UtcNow;
        }

        public void SetStatus(AppUser user, ExecuteBlankOrderType status)
        {
            BlankOrder.SetStatus(status);
            Updated = DateTime.UtcNow;
            BlankOrder.AddComment(user, "Change status");
        }

        public static ExecuteBlankOrder CreateEmptyBlankOrder()
        {
            return new ExecuteBlankOrder
            {
                Id = Guid.NewGuid(),
                User = new AppUser(),
                BlankOrder = new BlankOrder(),
                ExecuteStatus = ExecuteBlankOrderType.None
            };
        }
        public static ExecuteBlankOrder CreateBlankOrderExecute(AppUser user, BlankOrder blankOrder)
        {
            return new ExecuteBlankOrder()
            {
                Id = Guid.NewGuid(),
                User = user,
                BlankOrder = blankOrder,
                ExecuteStatus = ExecuteBlankOrderType.None,
            };
        }
        public static ExecuteBlankOrder CreateBlankOrderExecuteWithStatus(AppUser user, BlankOrder blankOrder, ExecuteBlankOrderType status)
        {
            return new ExecuteBlankOrder()
            {
                Id = Guid.NewGuid(),
                User = user,
                BlankOrder = blankOrder,
                ExecuteStatus = status,
            };
        }
    }
}
