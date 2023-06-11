using _1CService.Domain.Models;

namespace _1CService.Domain.Models
{
    public struct Comment
    {
        public int Index { get; set; }
        public string Author { get; set; }
        public string Commentary { get; set; }
        public DateTime Created { get; set; }
    }
}