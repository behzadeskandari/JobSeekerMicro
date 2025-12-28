using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class PricingPlan : EntityBase<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public string Currency { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string DurationUnit { get; set; } = string.Empty;
        public int JobCount { get; set; }
        public int? DiscountPercentage { get; set; }
        public string ButtonText { get; set; } = string.Empty;
        public bool? IsPopular { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public int PricingCategoryId { get; set; }
        public PricingCategory PricingCategory { get; set; } = null!;

        public ICollection<PricingFeature> Features { get; set; } = new List<PricingFeature>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
