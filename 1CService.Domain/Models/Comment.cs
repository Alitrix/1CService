using _1CService.Domain.Models;

namespace _1CService.Domain.Models
{
    public class Comment
    {
        public int CommentIndex { get; set; }
        public AppUser Author { get; set; }
        public string Commentary { get; set; }
        public readonly DateTime _createdAt;
        public DateTime CreatedAt => _createdAt;
        public Comment() => _createdAt = DateTime.UtcNow;

        public static Comment Create(AppUser user, string comment)
        {
            return new Comment()
            {
                Author = user,
                Commentary = comment
            };
        }
    }
}