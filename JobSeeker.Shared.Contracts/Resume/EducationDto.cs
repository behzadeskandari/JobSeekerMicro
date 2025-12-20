using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.Resume
{
    public class EducationDto
    {
        public Guid ResumeId { get; set; }
        public string Degree { get; set; }
        public string Institution { get; set; }
        public string Field { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}
