using _1CService.Application.Models.Requests.Queries;
using _1CService.Application.UseCases.BlankOrderHandler.Queries;
using _1CService.Controllers.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1CService.Controllers.Endpoints.BlankOrdersEP
{
    public static class BlankOrders
    {
        public static async Task<IResult> GetListBlankOrderHandler(IBlankOrderService blankOrderService, string place)
        {
            var lstBlanksOrder = await blankOrderService.GetList(new RequestBlankOrderList(place));

            return Results.Ok(lstBlanksOrder);
        }
    }
}
