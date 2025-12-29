using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class CompanyFollow : AuditableEntityBaseInt//AuditableEntityBase<Guid>
    {
        public string UserId { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int Rating { get; set; }
    }
}
