namespace _1CService.Domain.Domain
{
    public class CommentBlankOrder
    {
        public int CommentIndex { get; set; }
        public DateTime DateTime { get; set; }
        public AppUser Author { get; set; }
        public string Message { get; set; }

        public CommentBlankOrder(int index, AppUser user) 
        {
            CommentIndex = index;
            DateTime = DateTime.Now;
            Author = user;
            Message = "";
        }

        public void AddCommentToDoc(BlankOrder doc)
        {
            doc.AddComment(Author, Message);
        }
    }
}