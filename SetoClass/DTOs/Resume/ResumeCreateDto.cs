using SetoClass.DTOs.ResumeSkill;
using SetoClass.DTOs.ResumeSocial;
using System.ComponentModel.DataAnnotations;

namespace SetoClass.DTOs.Resume
{
    public class ResumeCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Profession { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string Certification { get; set; }

        [Required]
        public string ResumeImage { get; set; }

        [MaxLength(300)]
        public string CvUrl { get; set; }

        [Required]
        public ICollection<ResumeSkillCreateDto> Skills { get; set; }

        [Required]
        public ICollection<ResumeSocialCreateDto> Socials { get; set; }
    }

}
