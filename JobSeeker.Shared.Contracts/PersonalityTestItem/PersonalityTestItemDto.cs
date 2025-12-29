using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.PersonalityTestItem
{
    public class PersonalityTestItemDto
    {
        public Guid Id { get; set; }
        public int PersonalityTraitId { get; set; }
        public string ItemText { get; set; } // The statement or question
        public string ScoringDirection { get; set; } // e.g., 1 for positive correlation, -1 for negative
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }

    public class PersonalityTestSubmissionDto
    {
        public string UserId { get; set; }
        public List<PersonalityTestAnswerDto> Answers { get; set; }
    }

    public class PersonalityTestAnswerDto
    {
        public Guid PersonalityTestItemId { get; set; }
        public int Response { get; set; } // e.g., 1 to 5
    }
}
