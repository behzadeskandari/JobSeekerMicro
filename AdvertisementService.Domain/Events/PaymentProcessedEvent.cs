using JobSeeker.Shared.Kernel.Domain;

namespace AdvertisementService.Domain.Events
{
    public class PaymentProcessedEvent : DomainEvent
    {
        public Guid PaymentId { get; }
        public int AdvertisementId { get; }
        public string UserId { get; }
        public decimal Amount { get; }
        public string Status { get; }

        public PaymentProcessedEvent(Guid paymentId, int advertisementId, string userId, decimal amount, string status)
        {
            PaymentId = paymentId;
            AdvertisementId = advertisementId;
            UserId = userId;
            Amount = amount;
            Status = status;
        }
    }
}

