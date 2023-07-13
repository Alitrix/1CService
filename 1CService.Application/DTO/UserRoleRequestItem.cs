using _1CService.Application.Models;

namespace _1CService.Application.DTO
{
    public class UserRoleRequestItem
    {
        public required AppUser User { get; set; }
        public required string Role { get; set; }
        public Guid TokenGuid { get; set; }
    }
}
