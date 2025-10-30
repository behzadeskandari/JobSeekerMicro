using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Domain.Entities
{
    public class PricingFeature : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }

        public Guid PricingPlanId { get; set; }
        public PricingPlan PricingPlan { get; set; } = new PricingPlan();
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
