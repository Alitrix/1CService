using _1CService.Application.DTO;
using _1CService.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using _1CService.Application.Models.Responses.Queries;
using _1CService.Application.Models.Requests.Queries;
using _1CService.Application.Interfaces.Repositories;
using System.Collections.Generic;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Application.UseCases.BlankOrderHandler.Queries
{
    public class BlankOrderService : IBlankOrderService
    {
        private readonly IAsyncRepository<ListBlankOrderDTO> _repositiry;
        private readonly IMapper _mapper;

        public BlankOrderService(IAsyncRepository<ListBlankOrderDTO> repositiry, IMapper mapper) => (_repositiry, _mapper) = (repositiry, mapper);


        public async Task<ResponseBlankOrderDetailDTO> GetDetails(RequestBlankDetails request)
        {
            ListBlankOrderDTO blankOrder = await _repositiry.GetDetailAsync(new BlankOrderDetailDTO() 
            { 
                Number = request.Number, Date = request.Date 
            });

            return _mapper.Map<ResponseBlankOrderDetailDTO>(blankOrder);
        }

        public async Task<ResponseBlankOrderListDTO> GetList(RequestBlankOrderList request)
        {
            List<ListBlankOrderDTO> lstBlank = await _repositiry.ListAllAsync(new RequestBlankOrderListDTO()
            {
                WorkInPlace = "Офис"//request.WorkInPlace
            });

            return new ResponseBlankOrderListDTO() { Documents = lstBlank };
        }
    }
}
