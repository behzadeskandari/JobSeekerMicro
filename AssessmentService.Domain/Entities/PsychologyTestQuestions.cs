using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Models;

namespace AssessmentService.Domain.Entities
{
    public class PsychologyTestQuestion : IBaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("PsychologyTest")]
        public int PsychologyTestId { get; set; }
        public PsychologyTest PsychologyTest { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        [MaxLength(50)]
        public string QuestionType { get; set; } // e.g., MultipleChoice, RatingScale

        public string CorrectAnswer { get; set; } = string.Empty;// For objective questions

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? ScoringWeight { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }

        // Navigation property
        public ICollection<PsychologyTestResponse> PsychologyTestResponses { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; } = new();
    }
}
