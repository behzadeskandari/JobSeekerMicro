using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace AssessmentService.Domain.Entities
{

    public class PsychologyTestResult : IBaseEntity<Guid> , IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        //[ForeignKey("User")]
        public string UserId { get; set; }
        //public User User { get; set; }

        [Required]
        [ForeignKey("PsychologyTest")]
        public int PsychologyTestId { get; set; }
        public PsychologyTest PsychologyTest { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Required]
        public decimal TotalScore { get; set; }

        public List<PsychologyTestInterpretation>? Interpretation { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string ResultData { get; set; }
        public DateTime? DateTaken { get; set; }

        public bool? IsActive { get; set; }
        public ICollection<PsychologyTestResponse> Responses { get; set; }
        // Navigation property
        //[JsonIgnore]
        //public ICollection<Candidate> Candidates { get; set; }


        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
