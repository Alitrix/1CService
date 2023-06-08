using _1CService.Application.DTO.Requests.Queries;
using _1CService.Application.DTO.Responses.Queries;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.BlankOrder.Queries
{
    public interface IBlankOrderService
    {
        Task<ResponseBlankOrderListDTO> GetList(RequestBlankOrdersDTO request);
        Task<ResponseBlankOrderDetailDTO> GetDetails(RequestBlankDetailsDTO request);
    }
}
