using System.ComponentModel.DataAnnotations;

namespace SetoClass.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(300)]
        public required string Description { get; set; }
        public string GameAuthor { get; set; } = string.Empty;
        public string GameGenre { get; set; } = string.Empty;
        public int LikeCount { get; set; }
        public int SaveCount { get; set; }
        public DateTime ReleaseDate { get; set; }

        [MaxLength(300)]
        public required string CoverImageUrl { get; set; }

        [MaxLength(300)]
        public required string GameUrl { get; set; }
    }

}
