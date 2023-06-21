using _1CService.Application.Models.Requests.Command;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public interface IExecuteService
    {
        Task<bool> Create(RequestExecuteBlankOrder request);
    }
}
