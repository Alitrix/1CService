using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Models.Requests.Command;
using _1CService.Application.Models.Responses.Command;
using _1CService.Domain.Models;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public class CommentService : ICommentService
    {
        private readonly IBlankOrderRepository _repository;

        public CommentService(IBlankOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseBlankOrderMessageDTO> Create(RequestBlankOrderComment request)
        {
            var item = new BlankOrderCommentDTO()
            {
                Number = request.Number,
                Date = DateTime.Parse(request.Date).ToString(),
                Author = request.User1C,
                Comment = request.Comment
            };
            var response = await _repository.AddCommentAsync<ResponseBlankOrderMessageDTO>(item);
            return response;
        }
    }
}
