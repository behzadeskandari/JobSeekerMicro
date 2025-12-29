using System;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;
using JobService.Domain.Events;

namespace JobService.Domain.Entities
{
    public class InterviewDetail : AuditableEntityBaseInt//AuditableEntityBase<Guid>
    {
        [ForeignKey("JobApplication")]
        public int ApplicationId { get; set; }
        public virtual JobApplication JobApplication { get; set; }

        public int InterviewerId { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public string Outcome { get; set; }
    }
}
