using _1CService.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace _1CService.Application.Models
{
    public class AppUser : IdentityUser<string>
    {
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
        public static AppUser CreateUser(string email, string username)
        {
            return new AppUser()
            {
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
