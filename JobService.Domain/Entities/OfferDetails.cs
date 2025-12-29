using System;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class OfferDetails : AuditableEntityBaseInt//AuditableEntityBase<Guid>
    {
        [ForeignKey("JobApplication")]
        public int ApplicationId { get; set; }
        public virtual JobApplication JobApplication { get; set; }

        public string OfferedById { get; set; }
        public int CompanyId { get; set; }

        public DateTime OfferDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        public string Currency { get; set; }
        public string Benefits { get; set; }
        public string Status { get; set; }
    }
}
