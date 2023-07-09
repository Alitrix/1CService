
namespace _1CService.Application.Interfaces.Repositories
{
    public interface IAppUserDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
