using System.ComponentModel.DataAnnotations;

namespace SetoClass.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; } // New property

        [Required]
        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum UserRole
    {
        SuperAdmin,
        Admin,
        Author
    }
}
