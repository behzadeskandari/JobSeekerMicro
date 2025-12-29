using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class CompanyBenefit : AuditableEntityBaseInt//EntityBase<Guid>
    {
        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
