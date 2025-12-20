using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.Resume
{
    public class WorkExperienceDto
    {
        public Guid ResumeId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentJob { get; set; }
        public string Description { get; set; }
    }
}
