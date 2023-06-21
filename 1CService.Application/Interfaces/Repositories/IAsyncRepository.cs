﻿using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Repositories
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetDetailAsync(BlankOrderDetailDTO request);
        Task<IQueryable<T>> ListAllAsync(BlankOrderListDTO request);
        Task<bool> AddCommentAsync(BlankOrderCommentDTO comment);
        Task<bool> ExecuteAsync(BlankOrderExecuteDTO execute);
    }
}