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
    public class Execute
    {
        public Guid Id { get; set; }
        public AppUser User { get; set; }
        public BlankOrder BlankOrder { get; set; }
        public DateTime Created { get; set; }

        internal Execute(BlankOrder blankOrder, AppUser appUser)
        {
            BlankOrder = blankOrder;
            User = appUser;
            Created = DateTime.UtcNow;
        }

        public static Execute Create(AppUser user, BlankOrder blankOrder)
        {
            return new Execute(blankOrder, user)
            {
                Id = Guid.NewGuid(),
            };
        }
        public static Execute CreateWithStatus(AppUser user, BlankOrder blankOrder, ExecuteType status)
        {
            var newExecute = new Execute(blankOrder, user)
            {
                Id = Guid.NewGuid(),
            };
            newExecute.BlankOrder.SetStatus(status);
            newExecute.BlankOrder.AddComment(Comment.Create(user, status.ToString()));
            return newExecute;
        }
    }
}
