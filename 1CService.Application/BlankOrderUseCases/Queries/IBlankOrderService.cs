using _1CService.Application.DTO.Requests;
using _1CService.Application.DTO.Responses;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.BlankOrderUseCases.Queries
{
    public interface IBlankOrderService
    {
        Task<BlankOrderListDTO> GetList(RequestBlanksDTO request);
        Task<BlankOrderDetailDTO> GetDetails(RequestBlankDetailsDTO request);
    }
}
