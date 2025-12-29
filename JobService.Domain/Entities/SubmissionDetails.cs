using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class SubmissionDetails : AuditableEntityBaseInt
    {
        [ForeignKey("JobApplication")]
        public int ApplicationId { get; set; }
        public virtual JobApplication JobApplication { get; set; }

        public string Source { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
}
