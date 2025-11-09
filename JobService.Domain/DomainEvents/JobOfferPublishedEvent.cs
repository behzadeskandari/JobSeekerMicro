using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.DomainEvents
{
    public class JobOfferPublishedEvent : DomainEvent
    {
        public Guid JobOfferId { get; }
        public JobOfferPublishedEvent(Guid jobOfferId) => JobOfferId = jobOfferId;
    }
}
