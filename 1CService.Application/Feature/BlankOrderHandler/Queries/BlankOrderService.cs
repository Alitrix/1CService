using System.Linq;
using _1CService.Application.Interfaces;
using _1CService.Application.DTO;
using _1CService.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _1CService.Application.Models.Responses.Queries;
using _1CService.Application.Models.Requests.Queries;

namespace _1CService.Application.Feature.BlankOrderHandler.Queries
{
    public class BlankOrderService : IBlankOrderService
    {
        private readonly IAsyncRepository<BlankOrder> _repositiry;
        private readonly IMapper _mapper;

        public BlankOrderService(IAsyncRepository<BlankOrder> repositiry, IMapper mapper) => (_repositiry, _mapper) = (repositiry, mapper);


        public async Task<ResponseBlankOrderDetailDTO> GetDetails(RequestBlankDetails request)
        {
            BlankOrder blankOrder = await _repositiry.GetDetailAsync(new BlankOrderDetailDTO() { Number = request.Number, Date = request.Date });

            return _mapper.Map<ResponseBlankOrderDetailDTO>(blankOrder);
        }

        public async Task<ResponseBlankOrderListDTO> GetList(RequestBlankOrderList request)
        {
            IQueryable<BlankOrder> lstBlank = await _repositiry.ListAllAsync(new BlankOrderListDTO()
            {
                WorkInPlace = "Офис"//request.WorkInPlace
            });

            return new ResponseBlankOrderListDTO() { BlankOrders = lstBlank.ProjectTo<BlankOrderDTO>(_mapper.ConfigurationProvider).ToList() };
        }
    }
}
