using _1CService.Application.DTO;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application
{
    public interface IBlankOrderService
    {
        Task<BlankOrderListVM> GetList();
        Task<BlankOrderDTO> GetDetails(string number, string date);
        Task<bool> ExecuteBlankOrder(ExecuteBlankOrder executeBlankOrder);
        Task<bool> CreateComment(BlankOrderDTO dto, string comment);
    }
}
