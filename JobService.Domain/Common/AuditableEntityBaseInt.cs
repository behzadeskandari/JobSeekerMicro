using System.Collections.Generic;
using System.Collections.ObjectModel;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Common
{
    public abstract class AuditableEntityBaseInt : EntityBaseInt, IAggregateRoot
    {
        private readonly List<DomainEvent> _domainEvents = new();

        public IReadOnlyCollection<DomainEvent> DomainEvents => new ReadOnlyCollection<DomainEvent>(_domainEvents);

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void RaiseDomainEvent(DomainEvent domainEvent)
        {
            AddDomainEvent(domainEvent);
        }
    }
}

