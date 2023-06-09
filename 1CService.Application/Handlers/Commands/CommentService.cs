using _1CService.Application.DTO;
using _1CService.Application.DTO.Requests.Command;
using _1CService.Application.DTO.Responses.Command;
using _1CService.Application.Interfaces;
using _1CService.Domain.Models;
using _1CService.Utilities;

namespace _1CService.Application.Handlers.Commands
{
    public class CommentService : ICommentService
    {
        private readonly IService1C _repository;
        private readonly IAuthenticateRepositoryService _authenticateRepositoryService;

        public CommentService(IService1C repository, IAuthenticateRepositoryService authenticateRepositoryService)
        {
            _repository = repository;
            _authenticateRepositoryService = authenticateRepositoryService;
        }

        public async Task<bool> Create(RequestBlankOrderCommentDTO request, AppUser user)
        {
            var currentUser = await _authenticateRepositoryService.GetCurrentUser();
            var requestComment = new BlankOrderCommentDTO()
            {
                Author = currentUser.User1C,
                Comment = request.Comment,
                Date = request.Date,
                Number = request.Number,
            };

            var strParam = new StringContent(requestComment.ToJsonString());
            var responseComment = await _repository.PostAsync<ResponseBlankOrderMessageDTO>(_repository.InitTextContext(), "Comment", strParam);
            if (responseComment.ErrorCode == 0)
                return true;
            else
                return false;
        }
    }
}
