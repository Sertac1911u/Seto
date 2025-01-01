using System.ComponentModel.DataAnnotations;

namespace SetoClass.DTOs.Settings
{
    public class SettingsCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string SiteTitle { get; set; }

        public string FooterText { get; set; } = string.Empty;

        public string FaviconPath { get; set; } = string.Empty;

        [MaxLength(300)]
        public string SiteDescription { get; set; }

        [MaxLength(50)]
        public string PrimaryColor { get; set; }

        [MaxLength(50)]
        public string SecondaryColor { get; set; }

        public bool IsDarkModeEnabled { get; set; } = false;
    }

}
