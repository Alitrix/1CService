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

        public List<MaterialBlank> Materials { get; set; } = new List<MaterialBlank>();

        public List<ProductBlank> Products { get; set; } = new List<ProductBlank>();

        public List<CommentBlank> Taggeds { get; set; } = new List<CommentBlank>();
    }
}
