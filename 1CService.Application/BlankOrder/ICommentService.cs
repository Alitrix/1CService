using _1CService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.BlankOrder
{
    public interface ICommentService
    {
        Task<bool> Create(BlankOrderDTO dto, string comment);
    }
}
