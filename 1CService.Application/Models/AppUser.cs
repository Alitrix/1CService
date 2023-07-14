using _1CService.Domain.Enums;
using _1CService.Utilities;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.Models
{
    public class AppUser : IdentityUser<string>
    {
        public long CreatedAt { get; set; }
        public required string User1C { get; set; }
        public required string Password1C { get; set; }
        public WorkPlace WorkPlace { get; set; }
        public required string ServiceAddress { get; set; }
        public required string ServiceSection { get; set; }
        public required string ServiceBaseName { get; set; }

        public override string ToString()
        {
            return UserName?? "";
        }
        public static AppUser Create(string email, string username)
        {
            return new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow.Ticks,
                Email = email,
                UserName = username,
                User1C = "",
                Password1C = "",
                WorkPlace = WorkPlace.None,
                ServiceAddress = "srv",
                ServiceSection = "MobileService",
                ServiceBaseName = "smyk",
            };
        }
    }
}
