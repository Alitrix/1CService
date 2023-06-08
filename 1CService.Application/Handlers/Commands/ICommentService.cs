using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests.Command;
using _1CService.Domain.Models;

namespace _1CService.Application.Handlers.Commands
{
    public interface ICommentService
    {
        Task<bool> Create(RequestBlankOrderCommentDTO request, AppUser user);
    }
}
