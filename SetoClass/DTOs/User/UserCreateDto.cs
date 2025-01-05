using SetoClass.Models;
using System.ComponentModel.DataAnnotations;

namespace SetoClass.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        
    }
}
