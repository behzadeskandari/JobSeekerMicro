using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class JobApplication : AuditableEntityBaseInt//AuditableEntityBase<Guid>
    {
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }

        [ForeignKey("JobPost")]
        public int JobPostId { get; set; }
        public virtual JobPost JobPost { get; set; }

        public string UserId { get; set; }

        public DateTime ApplicationDate { get; set; }

        public string ResumeFileName { get; set; }
        public string ResumeFileUrl { get; set; }

        public string CoverLetter { get; set; }

        public string Status { get; set; }

        public SubmissionDetails SubmissionDetails { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<InterviewDetail> InterviewDetails { get; set; }

        public OfferDetails OfferDetails { get; set; }

        public RejectionDetails RejectionDetails { get; set; }
    }
}
