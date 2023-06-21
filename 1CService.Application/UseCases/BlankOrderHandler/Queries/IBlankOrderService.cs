using _1CService.Application.Models.Requests.Queries;
using _1CService.Application.Models.Responses.Queries;

namespace _1CService.Application.UseCases.BlankOrderHandler.Queries
{
    public interface IBlankOrderService
    {
        Task<ResponseBlankOrderListDTO> GetList(RequestBlankOrderList request);
        Task<ResponseBlankOrderDetailDTO> GetDetails(RequestBlankDetails request);
    }
}
