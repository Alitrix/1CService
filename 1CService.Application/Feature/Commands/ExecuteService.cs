using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using _1CService.Application.Models.Requests.Command;
using _1CService.Domain.Models;

namespace _1CService.Application.Feature.Commands
{
    public class ExecuteService : IExecuteService
    {
        private readonly IAsyncRepository<BlankOrder> _repository;

        public ExecuteService(IAsyncRepository<BlankOrder> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Create(RequestExecuteBlankOrder request)
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
