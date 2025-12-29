using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobSeeker.Shared.Contracts.PsychologyTest;
using JobSeeker.Shared.Contracts.PsychologyTestResponse;
namespace JobSeeker.Shared.Contracts.PsychologyTestQuestion
{
    public class PsychologyTestQuestionDto
    {
        public Guid Id { get; set; }

        [Required]
        public int PsychologyTestId { get; set; }
        public PsychologyTestDto PsychologyTest { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        [MaxLength(50)]
        public string QuestionType { get; set; } // e.g., MultipleChoice, RatingScale

        public string CorrectAnswer { get; set; } // For objective questions

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? ScoringWeight { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }

        // Navigation property
        public ICollection<PsychologyTestResponseDto> PsychologyTestResponses { get; set; }
        public List<AnswerOptionDto> AnswerOptions { get; set; } = new();
    }


    public class AnswerOptionDto
    {
        public int Id { get; set; }
        public int Value { get; set; }        // E.g., 1–4
        public string Label { get; set; }     // E.g., "Strongly Disagree"
    }
}
