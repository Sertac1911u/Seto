using System.ComponentModel.DataAnnotations;

namespace SetoClass.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Author { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public int Likes { get; set; } = 0;

        public int Comments { get; set; } = 0;

        public int Saves { get; set; } = 0;
        public int Views { get; set; } = 0;

        [MaxLength(300)]
        public required string ImageUrl { get; set; }
    }

}
