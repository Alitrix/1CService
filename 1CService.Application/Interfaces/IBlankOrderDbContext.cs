using _1CService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Interfaces
{
    public interface IBlankOrderDbContext
    {
        DbSet<BlankOrder> BlankOrders { get; set; }
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    }
}
