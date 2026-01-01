using JobSeeker.Shared.Kernel.Domain;

namespace AdvertisementService.Domain.Events
{
    public class SalesOrderCreatedEvent : DomainEvent
    {
        public int SalesOrderId { get; }
        public int CustomerId { get; }
        public bool IsPaid { get; }

        public SalesOrderCreatedEvent(int salesOrderId, int customerId, bool isPaid)
        {
            SalesOrderId = salesOrderId;
            CustomerId = customerId;
            IsPaid = isPaid;
        }
    }
}

