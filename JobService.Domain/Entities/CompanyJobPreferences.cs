using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class CompanyJobPreferences : AuditableEntityBaseInt//EntityBase<Guid>
    {
        [Required]
        [ForeignKey("JobPost")]
        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }

        public string PreferredSkills { get; set; }
        public string PreferredEducationLevel { get; set; }
        public string PreferredExperienceLevel { get; set; }
    }
}
