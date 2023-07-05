using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Models.BlankOrderModel.Request;
using _1CService.Application.Models.BlankOrderModel.Responses;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public class AcceptToWorkBlankOrder : IAcceptToWorkBlankOrder
    {
        private readonly IBlankOrderRepository _repository;

        public AcceptToWorkBlankOrder(IBlankOrderRepository repository) =>
            _repository = repository;

        public async Task<BlankOrderMessage> Create(AcceptToWorkBlankOrderCommand request)
        {
            return await _repository.AcceptInWorkAsync<BlankOrderMessage>(new BlankOrderExecuteDTOrepository()
            {
                Date = DateTime.Parse(request.Date).ToString(),
                Number = request.Number,
                Status = request.Status
            });
        }
    }
}
