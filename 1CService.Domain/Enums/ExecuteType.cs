using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Domain.Enums
{
    public enum ExecuteType : short
    {
        None = 0,
        Work = 1,
        Compleate = 2,
        Stoped = 3,
    }
    public static class ExtEnum
    {
        public static ExecuteType FromString(this int type) => type switch
        {
            0 => ExecuteType.None,
            1 => ExecuteType.Work,
            2 => ExecuteType.Compleate,
            3 => ExecuteType.Stoped,
            _ => throw new NotImplementedException(),
        };
    }
}
