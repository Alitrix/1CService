using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Models.Requests.Command;
using _1CService.Domain.Models;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public class CommentService : ICommentService
    {
        private readonly IAsyncRepository<BlankOrder> _repository;

        public CommentService(IAsyncRepository<BlankOrder> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Create(RequestBlankOrderComment request)
        {
            var item = new BlankOrderCommentDTO()
            {
                Number = request.Number,
                Date = request.Date,
                Author = request.User1C,
                Comment = request.Comment
            };
            return await _repository.AddCommentAsync(item);
        }
    }
}
