using _1CService.Domain.Enums;

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

        public static ExecuteBlankOrder Create(AppUser user, BlankOrder blankOrder)
        {
            return new ExecuteBlankOrder(blankOrder, user)
            {
                Id = Guid.NewGuid(),
            };
        }
        public static ExecuteBlankOrder CreateWithStatus(AppUser user, BlankOrder blankOrder, ExecuteType status)
        {
            var newExecute = new ExecuteBlankOrder(blankOrder, user)
            {
                Id = Guid.NewGuid(),
            };
            newExecute.BlankOrder.ExecuteState = (int)status;
            return newExecute;
        }
    }
}
