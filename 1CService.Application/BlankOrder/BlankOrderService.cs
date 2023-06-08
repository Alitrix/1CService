using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests;
using _1CService.Application.Interfaces;
using _1CService.Utilities;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1CService.Application.DTO.Responses;

namespace _1CService.Application.BlankOrder
{
    public class BlankOrderService : IBlankOrderService
    {
        private readonly IRepositoryService1C _repository;
        public BlankOrderService(IRepositoryService1C repository) => _repository = repository;

        public async Task<BlankOrderDetailDTO> GetDetails(RequestBlankDetailsDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var blank = await _repository.PostAsync<BlankOrderDetailDTO>(_repository.InitTextContext(), "Blank", strParam);
            return blank;
        }
        public async Task<BlankOrderListVM> GetList(RequestBlanksDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var lstBlankOrder = await _repository.PostAsync<List<BlankOrderDTO>>(_repository.InitJsonContext(), "Blanks", strParam); 

            return new BlankOrderListVM() { BlankOrders = lstBlankOrder };
        }
    }
}
