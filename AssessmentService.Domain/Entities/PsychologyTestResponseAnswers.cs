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
    public class PsychologyTestResponseAnswer : IBaseEntity<Guid> , IAggregateRoot
    {
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public int TestId { get; set; }

        [Required]
        [ForeignKey("PsychologyTestQuestion")]
        public Guid PsychologyTestQuestionId { get; set; }
        public PsychologyTestQuestion PsychologyTestQuestion { get; set; }
        [Required]
        public string Response { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Score { get; set; } // Calculated score for this response

        [Required]
        public DateTime SubmissionDate { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }



        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
