using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests.Command;
using _1CService.Application.Interfaces;
using _1CService.Domain.Models;

namespace _1CService.Application.Handlers.Commands
{
    public class ExecuteService : IExecuteService
    {
        private readonly IAsyncRepositiry<BlankOrder> _repository;

        public ExecuteService(IAsyncRepositiry<BlankOrder> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Create(RequestExecuteBlankOrderDTO request)
        {
            return await _repository.UpdateAsync(new BlankOrderExecuteDTO()
            {
                Date = request.Date,
                Number = request.Number,
                Status = request.Status
            });
        }
    }
}
