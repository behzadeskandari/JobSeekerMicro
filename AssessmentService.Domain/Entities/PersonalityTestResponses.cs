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
    public class PersonalityTestResponse : IBaseEntity<Guid>, IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        //[ForeignKey("User")]
        public string UserId { get; set; }
        //public User User { get; set; }

        [Required]
        [ForeignKey("PersonalityTestItem")]
        public Guid PersonalityTestItemId { get; set; }
        public PersonalityTestItem PersonalityTestItem { get; set; }

        [Required]
        public int Response { get; set; } // Assuming a Likert scale or similar

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
