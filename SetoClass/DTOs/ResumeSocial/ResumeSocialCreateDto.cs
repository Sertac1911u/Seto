using System.ComponentModel.DataAnnotations;

namespace SetoClass.DTOs.ResumeSocial
{
    public class ResumeSocialCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Platform { get; set; }

        [Required]
        [MaxLength(300)]
        public string Url { get; set; }
    }
}
