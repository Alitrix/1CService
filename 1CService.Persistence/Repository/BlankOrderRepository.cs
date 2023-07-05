﻿using _1CService.Application.DTO;
using _1CService.Utilities;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Enums;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Persistence.Repository
{
    public class BlankOrderRepository : IBlankOrderRepository
    {
        private readonly IService1C _service;

        public BlankOrderRepository(IService1C service) => _service = service;

        public async Task<T> GetDetailAsync<T>(BlankOrderDetailDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var lstBlankOrder = await _service.PostAsync<T>(await _service.InitContext(TypeContext1CService.Text), "Blank", strParam);
            return lstBlankOrder;
        }

        public async Task<List<T>> ListAllAsync<T>(RequestBlankOrderListDTO request)
        {
            StringContent strParam = new StringContent(request.ToJsonString());
            var lstBlankOrder = await _service.PostAsync<List<T>>(await _service.InitContext(TypeContext1CService.Text), "Blanks", strParam);
            return lstBlankOrder;
        }

        public async Task<T> AddCommentAsync<T>(BlankOrderCommentDTO comment)
        {
            StringContent strParamComment = new StringContent(comment.ToJsonString());
            var response = await _service.PostAsync<T>(await _service.InitContext(TypeContext1CService.Text), "Comment", strParamComment);
            //ResponseBlankOrderMessageDTO
            return response;
        }

        public async Task<T> AcceptInWorkAsync<T>(BlankOrderExecuteDTO execute)
        {
            StringContent strParamStatus = new StringContent(execute.ToJsonString());
            var response = await _service.PostAsync<T>(await _service.InitContext(TypeContext1CService.Text), "BlankStatus", strParamStatus);
            //ResponseBlankOrderMessageDTO
            return response;
        }
    }
}
