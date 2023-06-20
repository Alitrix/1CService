using _1CService.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace _1CService.Application.Interfaces.Repositories
{
    public interface IAppUserDbContext
    {
        DbSet<AppUser> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
