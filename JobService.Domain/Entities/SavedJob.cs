using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;
using JobService.Domain.Events;

namespace JobService.Domain.Entities
{
    public class SavedJob : AuditableEntityBaseInt
    {
        [ForeignKey("JobPost")]
        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }
        public string UserId { get; set; }
    }
}
