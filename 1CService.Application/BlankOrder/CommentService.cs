﻿using _1CService.Application.DTO;
using _1CService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.BlankOrder
{
    public class CommentService : ICommentService
    {
        private readonly IBlankOrderDbContext context;

        public CommentService(IBlankOrderDbContext _context) => context = _context;


        public Task<bool> Create(BlankOrderDTO dto, string comment)
        {
            throw new NotImplementedException();
        }
    }
}
