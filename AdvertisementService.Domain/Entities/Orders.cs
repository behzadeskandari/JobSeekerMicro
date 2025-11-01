using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Domain.Entities
{
    public class Order : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid PricingPlanId { get; set; }
        public PricingPlan PricingPlan { get; set; }
        public string? UserId { get; set; }
        //public User? User { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        //public Customer Customer { get; set; }
    }
}
