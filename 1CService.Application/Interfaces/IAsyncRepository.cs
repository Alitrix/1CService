using _1CService.Application.DTO;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetDetailAsync(BlankOrderDetailDTO request);
        Task<IQueryable<T>> ListAllAsync(BlankOrderListDTO request);
        Task<bool> AddCommentAsync(BlankOrderCommentDTO comment);
        Task<bool> UpdateAsync(BlankOrderExecuteDTO execute);
    }
}
