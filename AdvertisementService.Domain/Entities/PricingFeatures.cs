using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace AdvertisementService.Domain.Entities
{
    public class PricingFeature : IBaseEntity<Guid>  , IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }

        public Guid PricingPlanId { get; set; }
        public PricingPlan PricingPlan { get; set; } = new PricingPlan();
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }


        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
