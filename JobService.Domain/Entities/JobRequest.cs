using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobSeeker.Shared.Contracts.Enums;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class JobRequest : AuditableEntityBaseInt//AuditableEntityBase<Guid>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [ForeignKey("JobPost")]
        public int JobPostId { get; set; }

        public JobPost JobPost { get; set; }
        [Required]
        public string CoverLetter { get; set; }
        [Required]
        public string ResumeUrl { get; set; }

        public JobRequestStatus Status { get; set; } = JobRequestStatus.Start;
    }
}
