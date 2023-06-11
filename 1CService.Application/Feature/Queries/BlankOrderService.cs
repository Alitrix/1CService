using System.Linq;
using _1CService.Application.Interfaces;
using _1CService.Application.DTO;
using _1CService.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _1CService.Application.Models.Responses.Queries;
using _1CService.Application.Models.Requests.Queries;

namespace _1CService.Application.Feature.Queries
{
    public class BlankOrderService : IBlankOrderService
    {
        private readonly IAsyncRepositiry<BlankOrder> _repositiry;
        private readonly IMapper _mapper;

        public BlankOrderService(IAsyncRepositiry<BlankOrder> repositiry, IMapper mapper)
        {
            _repositiry = repositiry;
            _mapper = mapper;
        }

        public async Task<ResponseBlankOrderDetailDTO> GetDetails(RequestBlankDetails request)
        {
            var detail = await _repositiry.GetDetailAsync(new BlankOrderDetailDTO() { Number = request.Number, Date = request.Date });

            return _mapper.Map<ResponseBlankOrderDetailDTO>(detail);
        }

        public async Task<ResponseBlankOrderListDTO> GetList(RequestBlankOrderList request)
        {
            var lstBlank = await _repositiry.ListAllAsync(new BlankOrderListDTO()
            {
                WorkInPlace = request.WorkInPlace
            });

            return new ResponseBlankOrderListDTO() { BlankOrders = lstBlank.ProjectTo<BlankOrderDTO>(_mapper.ConfigurationProvider).ToList() };
        }
    }
}
