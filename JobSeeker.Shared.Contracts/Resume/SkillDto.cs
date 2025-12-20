using JobSeeker.Shared.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.Resume
{
    public class SkillDto
    {
        public Guid ResumeId { get; set; }
        public string Name { get; set; }
        public ProficiencyLevelEnum ProficiencyLevel { get; set; } // 1-5
    }
}
