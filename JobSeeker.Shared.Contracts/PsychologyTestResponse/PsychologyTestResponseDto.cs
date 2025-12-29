using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.PsychologyTestResponse
{
    public class PsychologyTestResponseDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public int PsychologyTestId { get; set; }

        [Required]
        public int PsychologyTestQuestionId { get; set; }
        public int TestResultId { get; set; }
        [Required]
        public string Response { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Score { get; set; } // Calculated score for this response
        [Required]
        public DateTime SubmissionDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
