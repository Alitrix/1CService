using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests.Command;
using _1CService.Application.DTO.Requests.Queries;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Interfaces
{
    public interface IAsyncRepositiry<T> where T : class
    {
        Task<IReadOnlySet<T>> GetDetailAsync(BlankOrderDetailDTO request);
        Task<IReadOnlyList<T>> ListAllAsync(BlankOrderListDTO request);
        Task<bool> AddCommentAsync(BlankOrderCommentDTO comment);
        Task<bool> UpdateAsync(BlankOrderExecuteDTO execute);
    }
}
