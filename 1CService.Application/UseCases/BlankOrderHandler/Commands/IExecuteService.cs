﻿using _1CService.Application.DTO;
using _1CService.Application.Models.Requests.Command;
using _1CService.Application.Models.Responses.Command;

namespace _1CService.Application.UseCases.BlankOrderHandler.Commands
{
    public interface IExecuteService
    {
        Task<ResponseBlankOrderMessageDTO> Create(RequestExecuteBlankOrder request);
    }
}
