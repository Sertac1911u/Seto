using System.ComponentModel.DataAnnotations;

namespace SetoClass.Models
{
    public class Resume
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Profession { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Education { get; set; }
        public required string Experience { get; set; }
        public required string Certification { get; set; }
        public required string ResumeImage { get; set; }

        [MaxLength(300)]
        public required string CvUrl { get; set; }

        // Navigation Properties
        public required ICollection<ResumeSkill> Skills { get; set; }
        public required ICollection<ResumeSocial> Socials { get; set; }
    }

}
