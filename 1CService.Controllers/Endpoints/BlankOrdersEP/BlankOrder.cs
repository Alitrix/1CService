using _1CService.Application.Models.Requests.Queries;
using _1CService.Application.UseCases.BlankOrderHandler.Queries;
using _1CService.Controllers.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.BlankOrdersEP
{
    public static class BlankOrder
    {
        public static async Task<IResult> GetListBlankOrderHandler(IGetBlankOrder blankOrderService, string PlaceOfWork)
        {
            var lstBlanksOrder = await blankOrderService.List(new RequestBlankOrderList(PlaceOfWork));

            return Results.Ok(lstBlanksOrder);
        }
        public static async Task<IResult> GetBlankOrderDetailHandler(IBlankOrderDetail blankOrderService, [FromBody] RequestBlankOrderDetail request)
        {
            var blankOrderDetail = await blankOrderService.Details(new RequestBlankDetails(request.Number, DateTime.Parse(request.Date).ToString()));

            return Results.Ok(blankOrderDetail);
        }
    }
}
