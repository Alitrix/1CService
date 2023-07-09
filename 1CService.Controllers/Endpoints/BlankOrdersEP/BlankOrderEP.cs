using _1CService.Application.Interfaces.UseCases;
using _1CService.Application.Models.BlankOrderModel.Request;
using _1CService.Controllers.Models.BlankOrder.Command;
using _1CService.Controllers.Models.BlankOrder.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.BlankOrdersEP
{
    public static class BlankOrderEP
    {
        public static async Task<IResult> GetListBlankOrderHandler(IGetBlankOrder blankOrderService, string PlaceOfWork)
        {
            var lstBlanksOrder = await blankOrderService.List(new BlankOrderListQuery(PlaceOfWork));

            return Results.Ok(lstBlanksOrder);
        }
        public static async Task<IResult> GetBlankOrderDetailHandler(IGetBlankOrderDetail blankOrderService, [FromBody] GetBlankOrderDetailDTO request)
        {
            var blankOrderDetail = await blankOrderService.Details(new BlankOrderDetailQuery(request.Number, DateTime.Parse(request.Date).ToString()));

            return Results.Ok(blankOrderDetail);
        }
        public static async Task<IResult> AcceptInWorkBlankOrderHandler(IAcceptToWorkBlankOrder executeService, AcceptInWorkBlankOrderDTO executeBlankOrder)
        {
            var retAccept = await executeService.Create(new AcceptToWorkBlankOrderCommand()
            {
                Date = executeBlankOrder.Date,
                Number = executeBlankOrder.Number,
                Status = executeBlankOrder.Status,
            });
            return Results.Ok(new 
            { 
                Success = retAccept
            });
        }
        public static async Task<IResult> AddCommentToBlankOrderHandler(IAddCommentToBlankOrder commentService, [FromBody] BlankOrderCommentDTO blankOrderComment)
        {
            var retAddComment = await commentService.Create(new AddCommentToBlankOrderCommand()
            {
                Number = blankOrderComment.Number,
                Date = blankOrderComment.Date,
                Comment = blankOrderComment.Comment,
            });

            return Results.Ok(new
            {
                Success = retAddComment
            });
        }
    }
}
