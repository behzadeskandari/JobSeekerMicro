using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Enums;

namespace JobSeeker.Shared.Contracts.Resume
{
    public class LanguageDto
    {
        public Guid ResumeId { get; set; }
        public string Name { get; set; }
        public ProficiencyLevelEnum ProficiencyLevel { get; set; } //
    }
}
