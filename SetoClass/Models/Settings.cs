using System.ComponentModel.DataAnnotations;

namespace SetoClass.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string SiteTitle { get; set; }
        public string FooterText { get; set; } = string.Empty;
        public string FaviconPath { get; set; } = string.Empty;

        [MaxLength(300)]
        public required string SiteDescription { get; set; }

        [MaxLength(50)]
        public required string PrimaryColor { get; set; }

        [MaxLength(50)]
        public required string SecondaryColor { get; set; }

        public bool IsDarkModeEnabled { get; set; } = false;
    }
}
