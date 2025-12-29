using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class JobTestAssignment : AuditableEntityBaseInt//EntityBase<Guid>
    {
        [ForeignKey("Job")]
        public int? JobId { get; set; }
        public Job Job { get; set; } 

        public int? PsychologyTestId { get; set; }
        public int? PersonalityTestId { get; set; } 

        [Required]
        public bool IsRequired { get; set; }
    }
}
