using _1CService.Application.Models.BlankOrderModel.Request;
using _1CService.Application.Models.BlankOrderModel.Responses;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public interface IAcceptToWorkBlankOrder
    {
        Task<BlankOrderMessage> Create(AcceptToWorkBlankOrderCommand request);
    }
}
