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

namespace JobService.Domain.Entities
{
    public class JobRequest : IBaseEntity<Guid>, IAggregateRoot
    {
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        //public User User { get; set; }

        [Required]
        [ForeignKey("JobPost")]
        public Guid JobPostId { get; set; }

        public JobPost JobPost { get; set; }
        [Required]
        public string CoverLetter { get; set; }
        [Required]
        public string ResumeUrl { get; set; }


        public JobRequestStatus Status { get; set; } = JobRequestStatus.Start;
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
