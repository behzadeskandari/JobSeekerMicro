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
    public class PersonalityTestResults : IBaseEntity<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        //public User User { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? OpennessScore { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? ConscientiousnessScore { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? ExtraversionScore { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? AgreeablenessScore { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? NeuroticismScore { get; set; }

        public List<PsychologyTestInterpretation> Interpretation { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }

        // Navigation property
        public ICollection<Guid> CandidatesIds { get; set; }

    }

}
