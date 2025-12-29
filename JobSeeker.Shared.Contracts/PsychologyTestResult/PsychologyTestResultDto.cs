using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.PsychologyTestResult
{
    public class PsychologyTestResultDto
    {
        public int Id { get; set; }
        public int PsychologyTestId { get; set; }
        public string PsychologyTestTitle { get; set; }
        public string UserId { get; set; }
        public decimal TotalScore { get; set; }
        public string Interpretation { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string ResultData { get; set; }
        public DateTime? DateTaken { get; set; }
        public bool IsActive { get; set; }
    }
}
