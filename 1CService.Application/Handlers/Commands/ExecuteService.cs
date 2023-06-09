using _1CService.Application.DTO.Requests.Command;
using _1CService.Application.Interfaces;
using _1CService.Domain.Enums;
using _1CService.Domain.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Handlers.Commands
{
    public class ExecuteService : IExecuteService
    {
        private readonly IService1C _repository;
        private readonly IAuthenticateRepositoryService _authenticateRepositoryService;

        public ExecuteService(IService1C repository, IAuthenticateRepositoryService authenticateRepositoryService)
        {
            _repository = repository;
            _authenticateRepositoryService = authenticateRepositoryService;
        }


        public async Task<bool> Handler(RequestExecuteBlankOrderDTO request)
        {
            BlankOrder blankOrder = new BlankOrder();
            ExecuteBlankOrder executeBlank = ExecuteBlankOrder.CreateWithStatus(await _authenticateRepositoryService.GetCurrentUser(), blankOrder, request.Status.FromString());

            throw new NotImplementedException();
        }
    }
}
