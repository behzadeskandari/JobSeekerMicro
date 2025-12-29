using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobSeeker.Shared.Contracts.Enums;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class Job : AuditableEntityBaseInt//AuditableEntityBase<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public JobLevel Level { get; set; }

        // Relations
        [Required]
        public int CompanyId { get; set; }
        public bool IsProirity { get; set; }
        public JobType JobType { get; set; }
        public string? JobDescription { get; set; }
        public string? JobRequirment { get; set; }
        public int? CityId { get; set; }
        [ForeignKey("TechnicalOption")]
        public int? TechnicalOptionsId { get; set; }
        [Required]
        [ForeignKey("JobCategory")]
        public int JobCategoryId { get; set; }

        public JobOfferStatus Status { get; set; }
        public virtual ICollection<JobApplication> JobApplication { get; set; }
        public ICollection<int> CandidatesIds { get; set; }
        public ICollection<JobPost> JobPosts { get; set; }
    }
}
