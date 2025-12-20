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
    public class PersonalityTestItem : IBaseEntity<Guid> , IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("PersonalityTrait")]
        public Guid PersonalityTraitId { get; set; }
        public PersonalityTrait PersonalityTrait { get; set; }

        [Required]
        public string ItemText { get; set; } // The statement or question

        public string ScoringDirection { get; set; } // e.g., 1 for positive correlation, -1 for negative
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }

        // Navigation property
        public ICollection<PersonalityTestResponse> PersonalityTestResponses { get; set; }
        public ICollection<Guid> JobTestAssignmentsIds { get; set; } = new List<Guid>();



        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
