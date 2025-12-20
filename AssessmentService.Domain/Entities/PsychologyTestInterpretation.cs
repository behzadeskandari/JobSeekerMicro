using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace AssessmentService.Domain.Entities
{
    public class PsychologyTestInterpretation : IBaseEntity<Guid> , IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("PsychologyTestResult")]
        public Guid? PsychologyTestResultId { get; set; } // Nullable to allow interpretations linked only to PsychologyTest
        public PsychologyTestResult PsychologyTestResult { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal MinScore { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal MaxScore { get; set; }

        [Required]
        public string Interpretation { get; set; } = string.Empty;
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateModified { get; set; }



        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
