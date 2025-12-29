using System;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;
using JobService.Domain.Events;

namespace JobService.Domain.Entities
{
    public class RejectionDetails : AuditableEntityBaseInt
    {
        [ForeignKey("JobApplication")]
        public int ApplicationId { get; set; }
        public virtual JobApplication JobApplication { get; set; }

        public string RejectedById { get; set; }

        public DateTime RejectionDate { get; set; }
        public string Reason { get; set; }
    }
}
