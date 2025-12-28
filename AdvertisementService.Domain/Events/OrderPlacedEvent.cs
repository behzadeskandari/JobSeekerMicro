using JobSeeker.Shared.Kernel.Domain;

namespace AdvertisementService.Domain.Events
{
    public class OrderPlacedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public int PricingPlanId { get; }
        public string? UserId { get; }
        public decimal TotalAmount { get; }

        public OrderPlacedEvent(Guid orderId, int pricingPlanId, string? userId, decimal totalAmount)
        {
            OrderId = orderId;
            PricingPlanId = pricingPlanId;
            UserId = userId;
            TotalAmount = totalAmount;
        }
    }
}

