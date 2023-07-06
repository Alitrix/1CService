using System.ComponentModel.DataAnnotations;

namespace _1CService.Controllers.Models.Auth
{
    public struct SignUpDTO
    {
        /// <summary>
        /// Почта пользователя
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required, MinLength(6)]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required, MinLength(6)]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
