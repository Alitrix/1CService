using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.BlankOrderModel.Request;
using _1CService.Application.Models.BlankOrderModel.Responses;

namespace _1CService.Application.UseCases.BlankOrderHandler.Queries
{
    public class GetBlankOrderDetail : IGetBlankOrderDetail
    {
        private readonly IBlankOrderRepository _repositiry;

        public GetBlankOrderDetail(IBlankOrderRepository repositiry) => 
            _repositiry = repositiry;


        public async Task<BlankOrderDetail> Details(BlankOrderDetailQuery request)
        {
            var blankOrder = await _repositiry.GetDetailAsync<BlankOrderDetail>(new BlankOrderDetailDTO()
            {
                Number = request.Number,
                Date = request.Date
            });

            return blankOrder;
        }
    }
}
