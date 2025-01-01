using System.ComponentModel.DataAnnotations;

namespace SetoClass.DTOs.Video
{
    public class VideoCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        public string Desc { get; set; }

        [Required]
        [MaxLength(300)]
        public string Author { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        public int Views { get; set; } = 0;

        public int Saves { get; set; } = 0;

        public int Likes { get; set; } = 0;

        public int Comments { get; set; } = 0;

        [MaxLength(300)]
        public string CoverImageUrl { get; set; }

        [MaxLength(300)]
        public string VideoUrl { get; set; }
    }

}
