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
    public class CompanyJobPreferences : IBaseEntity<Guid> ,IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("JobPost")]
        public Guid JobPostId { get; set; }
        public JobPost JobPost { get; set; }

        public string PreferredSkills { get; set; }
        public string PreferredEducationLevel { get; set; }
        public string PreferredExperienceLevel { get; set; }

        // Add other preference fields as needed
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
