using _1CService.Application.Interfaces;
using _1CService.Utilities;
using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests.Queries;
using _1CService.Application.DTO.Responses.Queries;

namespace _1CService.Application.BlankOrder.Queries
{
    public class BlankOrderService : IBlankOrderService
    {
        private readonly IRepositoryService1C _repository;
        public BlankOrderService(IRepositoryService1C repository) => _repository = repository;

        public async Task<ResponseBlankOrderDetailDTO> GetDetails(RequestBlankDetailsDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var blank = await _repository.PostAsync<ResponseBlankOrderDetailDTO>(_repository.InitTextContext(), "Blank", strParam);
            return blank;
        }
        public async Task<ResponseBlankOrderListDTO> GetList(RequestBlankOrdersDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var lstBlankOrder = await _repository.PostAsync<List<BlankOrderDTO>>(_repository.InitJsonContext(), "Blanks", strParam);

            return new ResponseBlankOrderListDTO() { BlankOrders = lstBlankOrder };
        }
    }
}
