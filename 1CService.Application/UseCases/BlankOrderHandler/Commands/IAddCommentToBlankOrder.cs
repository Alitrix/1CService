using _1CService.Application.Models.BlankOrderModel.Request;
using _1CService.Application.Models.BlankOrderModel.Responses;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public interface IAddCommentToBlankOrder
    {
        Task<BlankOrderMessage> Create(AddCommentToBlankOrderCommand request);
    }
}
