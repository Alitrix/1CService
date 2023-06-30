using _1CService.Application.DTO;
using _1CService.Application.Models.Responses.Command;
using _1CService.Utilities;
using Microsoft.AspNetCore.Http;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Enums;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Persistence.Repository
{
    public class BlankOrderRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly IService1C _service;

        public BlankOrderRepository(IService1C service) => _service = service;

        public async Task<T> GetDetailAsync(BlankOrderDetailDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var lstBlankOrder = await _service.PostAsync<T>(await _service.InitContext(TypeContext1CService.Json, new AppUser()), "Blank", strParam);
            return lstBlankOrder;
        }

        public async Task<List<T>> ListAllAsync(RequestBlankOrderListDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var lstBlankOrder = await _service.PostAsync<List<T>>(await _service.InitContext(TypeContext1CService.Json, new AppUser()), "Blanks", strParam);
            return lstBlankOrder;
        }

        public async Task<bool> AddCommentAsync(BlankOrderCommentDTO comment)
        {
            StringContent strParamComment = new StringContent(comment.ToJsonString());
            var response = await _service.PostAsync<ResponseBlankOrderMessageDTO>(await _service.InitContext(TypeContext1CService.Text, new AppUser()), "Comment", strParamComment);
            
            return response.ErrorCode == 0 ? true : false;
        }

        public async Task<bool> ExecuteAsync(BlankOrderExecuteDTO execute)
        {
            StringContent strParamStatus = new StringContent(execute.ToJsonString());
            var response = await _service.PostAsync<ResponseBlankOrderMessageDTO>(await _service.InitContext(TypeContext1CService.Text, new AppUser()), "BlankStatus", strParamStatus);

            return response.ErrorCode == 0 ? true : false;
        }
    }
}
