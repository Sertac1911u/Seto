using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SetoClass.Models
{
    public class ResumeSocial
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Platform { get; set; }

        [Required]
        [MaxLength(300)]
        public required string Url { get; set; }

        // Navigation Property
        [ForeignKey("Resume")]
        public int ResumeId { get; set; }
        public required Resume Resume { get; set; }
    }

}
