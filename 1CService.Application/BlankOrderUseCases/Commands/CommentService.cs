using _1CService.Application.DTO.Responses;
using _1CService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.BlankOrderUseCases.Commands
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryService1C context;

        public CommentService(IRepositoryService1C _context) => context = _context;


        public Task<bool> Create(BlankOrderDTO dto, string comment)
        {
            throw new NotImplementedException();
        }
    }
}
