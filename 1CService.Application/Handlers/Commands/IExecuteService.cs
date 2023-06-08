using _1CService.Application.DTO.Requests.Command;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Handlers.Commands
{
    public interface IExecuteService
    {
        Task<bool> Handler(RequestExecuteBlankOrderDTO request);
    }
}
