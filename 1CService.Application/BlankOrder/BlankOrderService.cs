using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.BlankOrder
{
    public class BlankOrderService : IBlankOrderService
    {
        private readonly IBlankOrderDbContext _repository;
        public BlankOrderService(IBlankOrderDbContext repository) => _repository = repository;

        public Task<BlankOrderDTO> GetDetails(string number, string date)
        {
            throw new NotImplementedException();
        }
        public Task<BlankOrderListVM> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
