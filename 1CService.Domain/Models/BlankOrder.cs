using _1CService.Domain.Enums;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Domain.Models
{
    public struct BlankOrder
    {
        public required string Number { get; init; }
        public required string Date { get; init; }
        public required string Manager { get; init; }
        public required string Contragent { get; init; }
        public int Urgency { get; set; }
        public required string CompletionDate { get; init; }
        public int ExecuteState { get; init; }
        public required byte[] ImagePreview { get; init; }
        public required List<Material> Materials { get; init; }
        public required List<Product> Products { get; init; }
        public required List<Comment> Comments { get; init; }
    }
}
