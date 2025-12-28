using System;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class Order : EntityBase<Guid>
    {
        public int PricingPlanId { get; set; }
        public PricingPlan PricingPlan { get; set; } = null!;
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        public string Status { get; set; } = string.Empty;
    }
}
