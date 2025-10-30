using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Domain.Entities
{
    public class AnswerOption : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int Value { get; set; }        // E.g., 1–4
        public string Label { get; set; }     // E.g., "Strongly Disagree"
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateModified { get; set; }

        public int PsychologyTestId { get; set; }
    }
}
