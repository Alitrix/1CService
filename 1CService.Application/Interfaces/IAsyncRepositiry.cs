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
        Task<T> GetDetailAsync(RequestBlankDetailsDTO request);
        Task<IReadOnlyList<T>> ListAllAsync(RequestBlankOrdersDTO request);
        Task<bool> AddCommentAsync(T entity, RequestBlankOrderCommentDTO comment);
        Task<bool> UpdateAsync(RequestExecuteBlankOrderDTO execute);
    }
}
