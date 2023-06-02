using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Domain.Domain
{
    public class BlankOrder
    {
        public string Nomer { get; set; } = string.Empty;

        public string Data { get; set; } = string.Empty;

        public string Manager { get; set; } = string.Empty;

        public string Contragent { get; set; } = string.Empty;

        public int Srochno { get; set; }

        public string CompletionDate { get; set; } = string.Empty;

        public string BlankStatus { get; set; } = string.Empty;

        public byte[] imagePreview { get; set; } =new byte[0];

        public List<MaterialBlankOrder> Materials { get; set; } = new List<MaterialBlankOrder>();

        public List<ProductBlankOrder> Products { get; set; } = new List<ProductBlankOrder>();

        public List<CommentBlankOrder> Comments { get; set; } = new List<CommentBlankOrder>();

        public BlankOrder() 
        { }

        public void AddComment(string author, string message)
        {
            Comments.Add(new CommentBlankOrder()
            {
                CommentIndex = Comments.Last().CommentIndex++,
                Author = author,
                DateTime = DateTime.Now.ToString(),
                Message = message
            });
        }
    }
}
