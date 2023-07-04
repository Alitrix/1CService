using _1CService.Application.DTO.Response;

namespace _1CService.Application.UseCases.Profile
{
    public interface IGetProfileAppUser
    {
        Task<ResponseGetProfile> Get();
    }
}