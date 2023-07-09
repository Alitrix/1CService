using _1CService.Application.DTO;
using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.BlankOrderModel.Request;
using _1CService.Application.Models.BlankOrderModel.Responses;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public class AddCommentToBlankOrder : IAddCommentToBlankOrder
    {
        private readonly IAppUserService _appUserService;
        private readonly IBlankOrderRepository _repository;

        public AddCommentToBlankOrder(IAppUserService appUserService, IBlankOrderRepository repository) =>
            (_appUserService, _repository) = (appUserService, repository);

        public async Task<BlankOrderMessage> Create(AddCommentToBlankOrderCommand request)
        {
            var currentUser = await _appUserService.GetCurrentUser();
            if (currentUser == null)
                return new BlankOrderMessage()
                {
                    ErrorCode = -400,
                    Message = "Error add comment",
                };

            var item = new BlankOrderCommentDTO()
            {
                Number = request.Number,
                Date = DateTime.Parse(request.Date).ToString(),
                Author = currentUser.User1C,
                Comment = request.Comment
            };
            var response = await _repository.AddCommentAsync<BlankOrderMessage>(item);
            return response;
        }
    }
}
