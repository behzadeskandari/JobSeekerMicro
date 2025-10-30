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
    public class PsychologyTestResultAnswer : IBaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TestId { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Required]
        public decimal TotalScore { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string ResultData { get; set; }
        public DateTime? DateTaken { get; set; }

        public bool? IsActive { get; set; }
        public List<PsychologyTestResponseAnswer> Responses { get; set; }
        // Navigation property
    }
}
