using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Domain.Entities
{
    public class JobApplication : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }

        [ForeignKey("Job")]
        public Guid JobId { get; set; }
        public virtual Job Job { get; set; } // Navigation property

        //[ForeignKey("Candidate")]
        public Guid CandidateId { get; set; }
        //public virtual Candidate Candidate { get; set; } // Navigation property

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

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
