using _1CService.Application.Models.Requests.Command;
using _1CService.Domain.Models;

namespace _1CService.Application.Feature.BlankOrderHandler.Commands
{
    public interface ICommentService
    {
        Task<bool> Create(RequestBlankOrderComment request);
    }
}
