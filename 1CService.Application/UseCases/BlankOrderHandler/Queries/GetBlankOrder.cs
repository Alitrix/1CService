using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Models.BlankOrderModel.Request;
using _1CService.Application.Models.BlankOrderModel.Responses;

namespace _1CService.Application.UseCases.BlankOrderHandler.Queries
{
    public class GetBlankOrder : IGetBlankOrder
    {
        private readonly IBlankOrderRepository _repositiry;

        public GetBlankOrder(IBlankOrderRepository repositiry) => _repositiry = repositiry;

        public async Task<BlankOrderList> List(BlankOrderListQuery request)
        {
            List<ListBlankOrderDTO> lstBlank = await _repositiry.ListAllAsync<ListBlankOrderDTO>(new RequestBlankOrderListDTO()
            {
                WorkInPlace = request.WorkInPlace
            });

            return new BlankOrderList() { Documents = lstBlank };
        }
    }
}
