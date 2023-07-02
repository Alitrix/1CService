using _1CService.Application.DTO;
using _1CService.Application.Models.Requests.Command;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public interface ICommentService
    {
        Task<bool> Create(RequestBlankOrderComment request);
    }
}
