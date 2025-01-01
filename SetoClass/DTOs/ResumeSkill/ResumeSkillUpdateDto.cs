using System.ComponentModel.DataAnnotations;

namespace SetoClass.DTOs.ResumeSkill
{
    public class ResumeSkillUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string SkillName { get; set; }

        [Required]
        public string SkillIcon { get; set; }
    }
}
