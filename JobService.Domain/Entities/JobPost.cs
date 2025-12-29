using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;
using JobService.Domain.Events;
using JobService.Domain.ValueObjects;

namespace JobService.Domain.Entities
{
    public class JobPost : AuditableEntityBaseInt//AuditableEntityBase<Guid>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string Requirements { get; set; }
        public int BenefitId { get; set; }

        [Required]
        public string Location { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public SalaryRange? Salary { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(60);
        public int? JobId { get; set; }

        public int? ViewCount { get; set; }
        public int? ApplicationCount { get; set; }
        public DateTime? DatePublished { get; set; }

        public string Source { get; set; }
        public string? ExternalJobBoardId { get; set; }
        public string SyncStatus { get; set; }
        public DateTime? LastSyncDate { get; set; }
        public ICollection<JobRequest> JobRequests { get; set; } = new List<JobRequest>();
        public ICollection<int> CompanyJobPreferenceIds { get; set; } = new List<int>();
        public IEnumerable<int> SkillIds { get; set; } = new List<int>();
        public int MinimumExperience { get; set; }
        public int? MinimumEducationLevelId { get; set; }
        public string MinimumEducationLevelDegree { get; set; }
        public string MinimumEducationLevelInstitution { get; set; }
        public string MinimumEducationLevelField { get; set; }
        public string MinimumEducationLevelDescription { get; set; }

        public int CityId { get; set; }
        public int? JobCategoryId { get; set; }
        public int? ProvinceId { get; set; }

        public void Publish()
        {
            IsActive = true;
            DatePublished = DateTime.UtcNow;
            RaiseDomainEvent(new JobPostPublishedEvent(Id, CompanyId, Title, JobCategoryId ?? 0, ProvinceId, CityId, UserId, DatePublished.Value));
        }

        public void Expire()
        {
            IsActive = false;
            RaiseDomainEvent(new JobPostExpiredEvent(Id, Title, ExpiresAt ?? DateTime.UtcNow));
        }

        // Helper property to get CompanyId from Job
        public int CompanyId { get; set; }
    }
}
