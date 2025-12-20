using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.Candidate
{
    public class CandidateCreateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? CoverLetter { get; set; }
        public string? ResumeUrl { get; set; }
        public string UserId { get; set; }
        public string? MBTIType { get; set; }
        public int YearsOfExperience { get; set; }
        public int EducationLevelId { get; set; }
        public int CityId { get; set; }
        public List<int> SkillIds { get; set; } = new List<int>();
    }
}
