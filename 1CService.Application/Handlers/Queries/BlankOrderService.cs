using _1CService.Application.Interfaces;
using _1CService.Utilities;
using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests.Queries;
using _1CService.Application.DTO.Responses.Queries;
using _1CService.Domain.Models;

namespace _1CService.Application.Handlers.Queries
{
    public class BlankOrderService : IBlankOrderService
    {
        private readonly IAsyncRepositiry<BlankOrder> _repositiry;

        public BlankOrderService(IAsyncRepositiry<BlankOrder> repositiry) => _repositiry = repositiry;

        public async Task<BlankOrder> GetDetails(RequestBlankDetailsDTO request)
        {
            return await _repositiry.GetDetailAsync(request);
        }


        public async Task<IReadOnlyList<BlankOrder>> GetList(string number, string date)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var lstBlankOrder = await _repository.PostAsync<List<BlankOrderDTO>>(_repository.InitJsonContext(), "Blanks", strParam);

            return new ResponseBlankOrderListDTO() { BlankOrders = lstBlankOrder };
        }

        public Task<ResponseBlankOrderListDTO> GetList(string WorkInPlace)
        {
            throw new NotImplementedException();
        }
    }
}
