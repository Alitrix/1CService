using _1CService.Application.Interfaces.Repositories;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Enums;
using _1CService.Application.DTO;
using _1CService.Utilities;

namespace _1CService.Persistence.Repository
{
    public class BlankOrderRepository : IBlankOrderRepository
    {
        private readonly IService1C _service;

        public BlankOrderRepository(IService1C service) => _service = service;

        public async Task<T?> GetDetailAsync<T>(BlankOrderDetailDTO request)
        {
            StringContent strParam = new (request.ToJsonString());
            var initContext = await _service.InitContext(TypeContext1CService.Text);
            if (initContext == null)
                return default;

            var lstBlankOrder = await _service.PostAsync<T>(initContext, "Blank", strParam);
            return lstBlankOrder;
        }

        public async Task<List<T>?> ListAllAsync<T>(RequestBlankOrderListDTO request)
        {
            var initContext = await _service.InitContext(TypeContext1CService.Text);
            if (initContext == null)
                return default;

            var lstBlankOrder = await _service.PostAsync<List<T>>(initContext, "Blanks", new StringContent(request.ToJsonString()));
            return lstBlankOrder;
        }

        public async Task<T?> AddCommentAsync<T>(BlankOrderCommentDTO comment)
        {
            var initContext = await _service.InitContext(TypeContext1CService.Text);
            if (initContext == null)
                return default;

            var response = await _service.PostAsync<T>(initContext, "Comment", new StringContent(comment.ToJsonString()));
            return response;
        }

        public async Task<T?> AcceptInWorkAsync<T>(BlankOrderExecuteDTOrepository execute)
        {
            var initContext = await _service.InitContext(TypeContext1CService.Text);
            if (initContext == null)
                return default;

            var response = await _service.PostAsync<T>(initContext, "BlankStatus", new StringContent(execute.ToJsonString()));
            return response;
        }
    }
}
