using JobSeeker.Shared.Kernel.Domain;

namespace AdvertisementService.Domain.Events
{
    public class SalesOrderCreatedEvent : DomainEvent
    {
        public Guid SalesOrderId { get; }
        public int CustomerId { get; }
        public bool IsPaid { get; }

        public SalesOrderCreatedEvent(Guid salesOrderId, int customerId, bool isPaid)
        {
            SalesOrderId = salesOrderId;
            CustomerId = customerId;
            IsPaid = isPaid;
        }
    }
}

