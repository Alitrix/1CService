using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using _1CService.Application.Models.Requests.Command;
using _1CService.Domain.Models;
using _1CService.Utilities;

namespace _1CService.Application.Feature.Commands
{
    public class CommentService : ICommentService
    {
        private readonly IAsyncRepository<BlankOrder> _repository;
        private readonly IAuthenticateRepositoryService _authenticateRepositoryService;

        public CommentService(IAsyncRepository<BlankOrder> repository, IAuthenticateRepositoryService authenticateRepositoryService)
        {
            _repository = repository;
            _authenticateRepositoryService = authenticateRepositoryService;
        }

        public async Task<bool> Create(RequestBlankOrderComment request)
        {
            var currentUser = await _authenticateRepositoryService.GetCurrentUser();
            var item = new BlankOrderCommentDTO()
            {
                Number = request.Number,
                Date = request.Date,
                Author = currentUser.User1C,
                Comment = request.Comment
            };
            return await _repository.AddCommentAsync(item);
        }
    }
}
