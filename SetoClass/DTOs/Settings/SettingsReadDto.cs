namespace SetoClass.DTOs.Settings
{
    public class SettingsReadDto
    {
        public int Id { get; set; }

        public string SiteTitle { get; set; }

        public string FooterText { get; set; }

        public string FaviconPath { get; set; }

        public string SiteDescription { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public bool IsDarkModeEnabled { get; set; }
    }

}
