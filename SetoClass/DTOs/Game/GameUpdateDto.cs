using System.ComponentModel.DataAnnotations;

namespace SetoClass.DTOs.Game
{
    public class GameUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public string GameAuthor { get; set; } = string.Empty;

        public string GameGenre { get; set; } = string.Empty;

        public int LikeCount { get; set; }

        public int SaveCount { get; set; }

        public DateTime ReleaseDate { get; set; }

        [MaxLength(300)]
        public string CoverImageUrl { get; set; }

        [MaxLength(300)]
        public string GameUrl { get; set; }
    }

}
