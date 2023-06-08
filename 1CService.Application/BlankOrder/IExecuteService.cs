using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.BlankOrder
{
    public interface IExecuteService
    {
        Task<bool> Handler(ExecuteBlankOrder executeBlankOrder);
    }
}
