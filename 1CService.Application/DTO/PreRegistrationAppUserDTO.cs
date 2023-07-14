using _1CService.Application.Models;

namespace _1CService.Application.DTO
{
    public class PreRegistrationAppUserDTO
    {
        public AppUser User { get; set; }
        public string EmailTokenConfirm { get; set; }
        public string Password { get; set; }

    }
}
