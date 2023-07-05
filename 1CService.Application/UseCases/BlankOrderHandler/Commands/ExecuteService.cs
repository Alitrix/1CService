using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Models.Requests.Command;
using _1CService.Application.Models.Responses.Command;
using _1CService.Domain.Models;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public class ExecuteService : IExecuteService
    {
        private readonly IBlankOrderRepository _repository;

        public ExecuteService(IBlankOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseBlankOrderMessageDTO> Create(RequestExecuteBlankOrder request)
        {
            return await _repository.AcceptInWorkAsync<ResponseBlankOrderMessageDTO>(new BlankOrderExecuteDTO()
            {
                Date = DateTime.Parse(request.Date).ToString(),
                Number = request.Number,
                Status = request.Status
            });
        }
    }
}
