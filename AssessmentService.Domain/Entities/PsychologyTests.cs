using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Enums;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace AssessmentService.Domain.Entities
{
    public class PsychologyTest : IBaseEntity<int> , IAggregateRoot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }

        public ICollection<PsychologyTestInterpretation> Interpretation { get; set; } = new List<PsychologyTestInterpretation>(); // interpreted results

        public string? UserId { get; set; }
        // Navigation properties
        public ICollection<PsychologyTestQuestion>? PsychologyTestQuestions { get; set; } = new List<PsychologyTestQuestion>();
        public ICollection<PsychologyTestResponse>? PsychologyTestResponses { get; set; } = new List<PsychologyTestResponse>();
        public ICollection<PsychologyTestResult>? PsychologyTestResults { get; set; } = new List<PsychologyTestResult>();
        public ICollection<Guid>? JobTestAssignmentsIds { get; set; } = new List<Guid>();
        public ICollection<PsychologyTestResponseAnswer>? PsychologyTestResponseAnswers { get; set; } = new List<PsychologyTestResponseAnswer>();

        public PsychologyTestType Type { get; set; }



        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
