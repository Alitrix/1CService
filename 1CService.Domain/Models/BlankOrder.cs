using _1CService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Domain.Domain
{
    public class BlankOrder
    {
        public string Number { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string Manager { get; set; } = string.Empty;

        public string Contragent { get; set; } = string.Empty;

        public int Urgency { get; set; }

        public string CompletionDate { get; set; } = string.Empty;

        public string BlankStatus { get; set; } = string.Empty;

        public byte[] ImagePreview { get; set; } = Array.Empty<byte>();

        public List<MaterialBlankOrder> Materials { get; set; }

        public List<ProductBlankOrder> Products { get; set; }

        public List<CommentBlankOrder> Comments { get; set; }

        public BlankOrder()
        {
            Materials = new List<MaterialBlankOrder>();
            Products = new List<ProductBlankOrder>();
            Comments = new List<CommentBlankOrder>();
        }
        public void AddComment(AppUser author, string message)
        {
            int lastIndex = Comments.Last().CommentIndex;
            Comments.Add(new CommentBlankOrder(lastIndex++, author)
            {
                Author = author,
                Message = message
            });
        }
    }
}
