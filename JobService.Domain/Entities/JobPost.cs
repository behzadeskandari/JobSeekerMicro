using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;
using JobService.Domain.DomainEvents;
using JobService.Domain.ValueObjects;

namespace JobService.Domain.Entities
{
    public class JobPost : IBaseEntity<Guid> , IAggregateRoot
    {

        public List<DomainEvent> DomainEvents { get; private set; } = new List<DomainEvent>();


        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string Requirements { get; set; }
        public Guid BenefitId { get; set; }

        [Required]
        public string Location { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public SalaryRange? Salary { get; set; }

        [Required]
        //[ForeignKey("Staff")]
        public string UserId { get; set; }

        //public User Staff { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiresAt { get; set; } = DateTime.Now.AddDays(60);
        public bool? IsActive { get; set; } = true;
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        //[ForeignKey("Job")]
        public Guid? JobId { get; set; }
        //public Job? Job { get; set; }

        public int? ViewCount { get; set; }
        public int? ApplicationCount { get; set; }
        public DateTime? DatePublished { get; set; }

        public string Source { get; set; }
        public string? ExternalJobBoardId { get; set; }
        public string SyncStatus { get; set; }
        public DateTime? LastSyncDate { get; set; }
        public ICollection<JobRequest> JobRequests { get; set; } = new List<JobRequest>();
        public ICollection<Guid> CompanyJobPreferenceIds { get; set; } = new List<Guid>();
        public IEnumerable<Guid> SkillIds { get; set; } = new List<Guid>();
        public int MinimumExperience { get; set; } //Years
        public Guid? MinimumEducationLevelId { get; set; } //Years
        public string MinimumEducationLevelDegree { get; set; }
        public string MinimumEducationLevelInstitution { get; set; }
        public string MinimumEducationLevelField { get; set; }
        public string MinimumEducationLevelDescription { get; set; }

        //[ForeignKey("City")]
        public int CityId { get; set; }
        //public City City { get; set; }

        public void Publish()
        {
            IsActive = true;
            DatePublished = DateTime.Now;
            Raise(new JobOfferPublishedEvent(Id));
        }

        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }

}
