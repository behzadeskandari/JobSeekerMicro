using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class PricingFeature : EntityBase<Guid>
    {
        public string Description { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;

        public int PricingPlanId { get; set; }
        public PricingPlan PricingPlan { get; set; } = null!;
    }
}
