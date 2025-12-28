using System.Collections.Generic;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class PricingCategory : EntityBaseInt
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public List<PricingPlan> Plans { get; set; } = new List<PricingPlan>();
    }
}
