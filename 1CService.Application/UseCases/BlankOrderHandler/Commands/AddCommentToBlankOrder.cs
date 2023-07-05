using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Models.BlankOrderModel.Request;
using _1CService.Application.Models.BlankOrderModel.Responses;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public class AddCommentToBlankOrder : IAddCommentToBlankOrder
    {
        private readonly IBlankOrderRepository _repository;

        public AddCommentToBlankOrder(IBlankOrderRepository repository) =>
            _repository = repository;

        public async Task<BlankOrderMessage> Create(AddCommentToBlankOrderCommand request)
        {
            var item = new BlankOrderCommentDTO()
            {
                Number = request.Number,
                Date = DateTime.Parse(request.Date).ToString(),
                Author = request.User1C,
                Comment = request.Comment
            };
            var response = await _repository.AddCommentAsync<BlankOrderMessage>(item);
            return response;
        }
    }
}
