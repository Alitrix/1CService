using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests;
using _1CService.Application.DTO.Responses;
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
        Task<BlankOrderListVM> GetList(RequestBlanksDTO request);
        Task<BlankOrderDetailDTO> GetDetails(RequestBlankDetailsDTO request);
    }
}
