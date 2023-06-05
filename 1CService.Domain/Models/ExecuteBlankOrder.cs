using _1CService.Domain.Enums;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Domain.Models
{
    public class ExecuteBlankOrder
    {
        public Guid Id { get; set; }
        public AppUser User { get; set; }
        public BlankOrder BlankOrder { get; set; }
        public DateTime Created { get; set; }

        internal ExecuteBlankOrder(BlankOrder blankOrder, AppUser appUser)
        {
            BlankOrder = blankOrder;
            User = appUser;
            Created = DateTime.UtcNow;
        }

        public static ExecuteBlankOrder CreateBlankOrderExecute(AppUser user, BlankOrder blankOrder)
        {
            return new ExecuteBlankOrder(blankOrder, user)
            {
                Id = Guid.NewGuid(),
            };
        }
        public static ExecuteBlankOrder CreateBlankOrderExecuteWithStatus(AppUser user, BlankOrder blankOrder, ExecuteType status)
        {
            var newExecute = new ExecuteBlankOrder(blankOrder, user)
            {
                Id = Guid.NewGuid(),
            };
            blankOrder.AddComment(Comment.Create(user, status.ToString()));
            return newExecute;
        }
    }
}
