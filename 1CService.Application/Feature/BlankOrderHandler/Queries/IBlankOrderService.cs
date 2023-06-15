using _1CService.Application.DTO;
using _1CService.Application.Models.Requests.Queries;
using _1CService.Application.Models.Responses.Queries;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Feature.BlankOrderHandler.Queries
{
    public interface IBlankOrderService
    {
        Task<ResponseBlankOrderListDTO> GetList(RequestBlankOrderList request);
        Task<ResponseBlankOrderDetailDTO> GetDetails(RequestBlankDetails request);
    }
}
