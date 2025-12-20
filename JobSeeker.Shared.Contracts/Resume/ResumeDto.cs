using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.Resume
{
    public class ResumeDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Summary { get; set; }

        public ICollection<WorkExperienceDto> WorkExperiences { get; set; }
        public ICollection<EducationDto> Educations { get; set; }
        public ICollection<SkillDto> Skills { get; set; }
        public ICollection<LanguageDto> Languages { get; set; }
    }
}
