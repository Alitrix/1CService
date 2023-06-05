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
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsWork => BlankOrder.BlankStatus.Equals(ExecuteBlankOrderType.Work.ToString());

        public ExecuteBlankOrder(BlankOrder blankOrder, AppUser appUser)
        {
            BlankOrder = blankOrder;
            User = appUser;
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
            return new ExecuteBlankOrder(new BlankOrder(), new AppUser())
            {
                Id = Guid.NewGuid(),
            };
        }
        public static ExecuteBlankOrder CreateBlankOrderExecute(AppUser user, BlankOrder blankOrder)
        {
            return new ExecuteBlankOrder(blankOrder, user)
            {
                Id = Guid.NewGuid(),
            };
        }
        public static ExecuteBlankOrder CreateBlankOrderExecuteWithStatus(AppUser user, BlankOrder blankOrder, ExecuteBlankOrderType status)
        {
            var newExecute = new ExecuteBlankOrder(blankOrder, user)
            {
                Id = Guid.NewGuid(),
            };
            newExecute.SetStatus(user, status);
            return newExecute;
        }
    }
}
