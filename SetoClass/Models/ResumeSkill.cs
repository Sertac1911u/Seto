using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SetoClass.Models
{
    public class ResumeSkill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string SkillName { get; set; }
        public required string SkillIcon { get; set; }


        [ForeignKey("Resume")]
        public int ResumeId { get; set; }
        public required Resume Resume { get; set; }
    }

}
