using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;
using MassTransit.Transports;

namespace AdvertisementService.Domain.Entities
{
    public class PricingPlan : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int Duration { get; set; }
        public string DurationUnit { get; set; }
        public int JobCount { get; set; }
        public int? DiscountPercentage { get; set; }
        public string ButtonText { get; set; }
        public bool? IsPopular { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public Guid PricingCategoryId { get; set; }
        public PricingCategory PricingCategory { get; set; }

        public ICollection<PricingFeature> Features { get; set; } = new List<PricingFeature>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
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
