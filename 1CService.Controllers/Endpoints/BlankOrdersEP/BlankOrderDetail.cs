using _1CService.Application.DTO;
using _1CService.Application.Models.Requests.Queries;
using _1CService.Application.UseCases.BlankOrderHandler.Queries;
using _1CService.Controllers.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Controllers.Endpoints.BlankOrdersEP
{
    public static class BlankOrderDetail
    {
        public static async Task<IResult> GetBlankOrderDetailHandler(IBlankOrderService blankOrderService, [FromBody] RequestBlankOrderDetail request)
        {
            var blankOrderDetail = await blankOrderService.GetDetails(new RequestBlankDetails(request.Number, request.Date));

            return Results.Ok(blankOrderDetail);
        }
    }
}
