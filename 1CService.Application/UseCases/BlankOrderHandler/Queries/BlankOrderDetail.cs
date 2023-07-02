using _1CService.Application.DTO;
using _1CService.Domain.Models;
using AutoMapper;
using _1CService.Application.Models.Responses.Queries;
using _1CService.Application.Models.Requests.Queries;
using _1CService.Application.Interfaces.Repositories;

namespace _1CService.Application.UseCases.BlankOrderHandler.Queries
{
    public class BlankOrderDetail : IBlankOrderDetail
    {
        private readonly IAsyncRepository<BlankOrder> _repositiry;
        private readonly IMapper _mapper;

        public BlankOrderDetail(IAsyncRepository<BlankOrder> repositiry, IMapper mapper) => (_repositiry, _mapper) = (repositiry, mapper);


        public async Task<ResponseBlankOrderDetailDTO> Details(RequestBlankDetails request)
        {
            var blankOrder = await _repositiry.GetDetailAsync(new BlankOrderDetailDTO()
            {
                Number = request.Number,
                Date = request.Date
            });

            return _mapper.Map<ResponseBlankOrderDetailDTO>(blankOrder);
        }
    }
}
