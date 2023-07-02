using _1CService.Domain.Enums;
using _1CService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Domain.Models
{
    public class BlankOrder
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Manager { get; set; }
        public string Contragent { get; set; }
        public int Urgency { get; set; }
        public string CompletionDate { get; set; }
        public int ExecuteState { get; set; }
        public byte[] imagePreview { get; set; }
        public List<Material> Materials { get; set; }
        public List<Product> Products { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
