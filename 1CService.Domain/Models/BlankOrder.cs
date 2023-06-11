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
        public string Number { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string Manager { get; set; } = string.Empty;

        public string Contragent { get; set; } = string.Empty;

        public int Urgency { get; set; }

        public string CompletionDate { get; set; }

        public int ExecuteState { get; set; }

        public byte[] ImagePreview { get; set; } = Array.Empty<byte>();

        public IList<Material> Materials { get; set; }

        public IList<Product> Products { get; set; }

        public IList<Comment> Comments { get; set; }

        public BlankOrder()
        {
            Materials = new List<Material>();
            Products = new List<Product>();
            Comments = new List<Comment>();
            ExecuteState = -1;
            CompletionDate = "";
        }
    }
}
