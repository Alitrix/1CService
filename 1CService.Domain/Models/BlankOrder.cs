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

        public string ExecuteState { get; set; }

        public byte[] ImagePreview { get; set; } = Array.Empty<byte>();

        public List<Material> Materials { get; set; }

        public List<Product> Products { get; set; }

        public List<Comment> Comments { get; set; }

        public BlankOrder()
        {
            Materials = new List<Material>();
            Products = new List<Product>();
            Comments = new List<Comment>();
            ExecuteState = "";
            CompletionDate = "";
        }
        public int GetCommentLastIndex()
        {
            return Comments.Last().CommentIndex;
        }
        public void SetStatus(ExecuteType status)
        {
            ExecuteState = status.ToString();
        }
        public void AddComment(Comment comment)
        {
            var lastIndex = GetCommentLastIndex();
            comment.CommentIndex = lastIndex++;
            if(Comments != null)
            {
                Comments.Add(comment);
            }
        }
    }
}
