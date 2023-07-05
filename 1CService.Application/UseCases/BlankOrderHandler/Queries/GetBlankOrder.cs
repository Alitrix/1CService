using _1CService.Application.DTO;
using AutoMapper;
using _1CService.Application.Models.Responses.Queries;
using _1CService.Application.Models.Requests.Queries;
using _1CService.Application.Interfaces.Repositories;

namespace _1CService.Application.UseCases.BlankOrderHandler.Queries
{
    public class GetBlankOrder : IGetBlankOrder
    {
        private readonly IBlankOrderRepository _repositiry;
        private readonly IMapper _mapper;

        public GetBlankOrder(IBlankOrderRepository repositiry, IMapper mapper) => (_repositiry, _mapper) = (repositiry, mapper);

        public async Task<ResponseBlankOrderListDTO> List(RequestBlankOrderList request)
        {
            List<ListBlankOrderDTO> lstBlank = await _repositiry.ListAllAsync<ListBlankOrderDTO>(new RequestBlankOrderListDTO()
            {
                WorkInPlace = request.WorkInPlace
            });

            return new ResponseBlankOrderListDTO() { Documents = lstBlank };
        }
    }
}
