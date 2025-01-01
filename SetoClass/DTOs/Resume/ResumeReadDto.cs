using SetoClass.DTOs.ResumeSkill;
using SetoClass.DTOs.ResumeSocial;

namespace SetoClass.DTOs.Resume
{
    public class ResumeReadDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Profession { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Education { get; set; }

        public string Experience { get; set; }

        public string Certification { get; set; }

        public string ResumeImage { get; set; }

        public string CvUrl { get; set; }

        public ICollection<ResumeSkillReadDto> Skills { get; set; }

        public ICollection<ResumeSocialReadDto> Socials { get; set; }
    }

}
