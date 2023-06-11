using _1CService.Application.Interfaces;
using _1CService.Utilities;
using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests.Queries;
using _1CService.Application.DTO.Responses.Queries;
using _1CService.Domain.Models;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;

namespace _1CService.Application.Handlers.Queries
{
    public class BlankOrderService : IBlankOrderService
    {
        private readonly IAsyncRepositiry<BlankOrder> _repositiry;

        public BlankOrderService(IAsyncRepositiry<BlankOrder> repositiry) => _repositiry = repositiry;

        public async Task<IReadOnlySet<BlankOrder>> GetDetails(string number, string date)
        {
            return await _repositiry.GetDetailAsync(new BlankOrderDetailDTO() { Number = number, Date = date });//Mapped to DTO
        }

        public async Task<IReadOnlyList<BlankOrder>> GetList(string workplace)
        {
            return await _repositiry.ListAllAsync(new BlankOrderListDTO()
            {
                WorkInPlace = workplace
            });//Mapped to DTO
        }
    }
}
