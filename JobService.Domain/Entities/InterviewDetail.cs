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

namespace JobService.Domain.Entities
{
    public class InterviewDetail : IBaseEntity<Guid> ,IAggregateRoot
    {
        [Key]
        public int InterviewId { get; set; }

        [ForeignKey("JobApplication")]
        public Guid ApplicationId { get; set; }
        public virtual JobApplication JobApplication { get; set; }

        [ForeignKey("Interviewer")]
        public Guid InterviewerId { get; set; }
        // Assuming you have a User entity for interviewers
        // public virtual User Interviewer { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public string Outcome { get; set; }
        public Guid Id { get; set; }
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
