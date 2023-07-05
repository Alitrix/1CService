using _1CService.Application.DTO;

namespace _1CService.Application.Interfaces.Repositories
{
    public interface IBlankOrderRepository
    {
        Task<T> AcceptInWorkAsync<T>(BlankOrderExecuteDTO execute);
        Task<T> AddCommentAsync<T>(BlankOrderCommentDTO comment);
        Task<T> GetDetailAsync<T>(BlankOrderDetailDTO request);
        Task<List<T>> ListAllAsync<T>(RequestBlankOrderListDTO request);
    }
}