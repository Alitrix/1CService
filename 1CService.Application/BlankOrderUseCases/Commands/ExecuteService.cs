using _1CService.Application.Interfaces;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.BlankOrderUseCases.Commands
{
    public class ExecuteService : IExecuteService
    {
        private readonly IRepositoryService1C _context;

        public ExecuteService(IRepositoryService1C context) => _context = context;


        public Task<bool> Handler(ExecuteBlankOrder executeBlankOrder)
        {
            throw new NotImplementedException();
        }
    }
}
