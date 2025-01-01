using System.ComponentModel.DataAnnotations;

namespace SetoClass.DTOs.Blog
{
    public class BlogCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [MaxLength(300)]
        public string ImageUrl { get; set; }
    }

}
