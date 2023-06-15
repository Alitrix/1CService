using _1CService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Interfaces
{
    public interface ISettings1CService
    {
        Task<Settings> GetHttpServiceSettings();
    }
}
