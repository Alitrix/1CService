using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests.Command;
using _1CService.Application.DTO.Requests.Queries;
using _1CService.Application.DTO.Responses.Command;
using _1CService.Application.Interfaces;
using _1CService.Domain.Models;
using _1CService.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Persistence.Repository
{
    public class BlankOrderRepository<T> : IAsyncRepositiry<T> where T : class
    {
        private readonly IService1C _service;

        public BlankOrderRepository(IService1C service) => _service = service;

        public async Task<T> GetDetailAsync(RequestBlankDetailsDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var lstBlankOrder = await _service.PostAsync<T>(_service.InitJsonContext(), "Blank", strParam);
            return lstBlankOrder;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(RequestBlankOrdersDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var lstBlankOrder = await _service.PostAsync<IReadOnlyList<T>>(_service.InitJsonContext(), "Blanks", strParam);
            return lstBlankOrder;
        }

        public async Task<bool> AddCommentAsync(T entity, RequestBlankOrderCommentDTO comment)
        {
            StringContent strParamComment = new StringContent(comment.ToJsonString());
            var response = await _service.PostAsync<ResponseBlankOrderMessageDTO>(_service.InitTextContext(), "Comment", strParamComment);
            
            return response.ErrorCode == 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(RequestExecuteBlankOrderDTO execute)
        {
            StringContent strParamStatus = new StringContent(execute.ToJsonString());
            var response = await _service.PostAsync<ResponseBlankOrderMessageDTO>(_service.InitTextContext(), "BlankStatus", strParamStatus);

            return response.ErrorCode == 0 ? true : false;
        }
    }
}
