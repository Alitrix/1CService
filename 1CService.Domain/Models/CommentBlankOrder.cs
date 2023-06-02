namespace _1CService.Domain.Domain
{
    public class CommentBlankOrder
    {
        public int CommentIndex { get; set; }
        public string DateTime { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}