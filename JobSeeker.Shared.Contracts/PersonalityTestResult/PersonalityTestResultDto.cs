using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.PersonalityTestResult
{
    public class PersonalityTestResultDto
    {
        public int Id { get; set; }
        public int PersonalityTestId { get; set; }
        public string PersonalityTestTitle { get; set; }
        public string UserId { get; set; }
        public string ResultData { get; set; }
        public DateTime? DateTaken { get; set; }
        public bool IsActive { get; set; }

        public DateTime SubmissionDate { get; set; }
        public decimal? OpennessScore { get; set; }
        public decimal? ConscientiousnessScore { get; set; }
        public decimal? ExtraversionScore { get; set; }
        public decimal? AgreeablenessScore { get; set; }
        public decimal? NeuroticismScore { get; set; }
        public string Interpretation { get; set; }
    }
}
