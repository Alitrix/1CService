using _1CService.Application.Interfaces;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.BlankOrder
{
    public class ExecuteService : IExecuteService
    {
        private readonly IBlankOrderDbContext context;

        public ExecuteService(IBlankOrderDbContext _context) => context = _context;


        public Task<bool> Create(Execute executeBlankOrder)
        {
            throw new NotImplementedException();
        }
    }
}
