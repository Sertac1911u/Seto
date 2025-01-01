using System.ComponentModel.DataAnnotations;

namespace SetoClass.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        public required string Desc { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(300)]
        public required string Author { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        public int Views { get; set; } = 0;
        public int Saves { get; set; } = 0;
        public int Likes { get; set; } = 0;

        public int Comments { get; set; } = 0;

        [MaxLength(300)]
        public required string CoverImageUrl { get; set; }

        [MaxLength(300)]
        public required string VideoUrl { get; set; }
    }

}
