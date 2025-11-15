using JobSeeker.Shared.Kernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.DomainEvents
{
    public class JobOfferPublishedEvent : DomainEvent
    {
        public Guid JobOfferId { get; }
        public JobOfferPublishedEvent(Guid jobOfferId) => JobOfferId = jobOfferId;
    }
}
