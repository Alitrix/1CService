using _1CService.Application.Models.BlankOrderModel.Request;
using _1CService.Application.Models.BlankOrderModel.Responses;

namespace _1CService.Application.UseCases.BlankOrderHandler.Queries
{
    public interface IGetBlankOrderDetail
    {
        Task<BlankOrderDetail> Details(BlankOrderDetailQuery request);
    }
}