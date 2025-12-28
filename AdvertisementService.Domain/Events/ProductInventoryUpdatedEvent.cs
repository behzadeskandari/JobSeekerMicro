using JobSeeker.Shared.Kernel.Domain;

namespace AdvertisementService.Domain.Events
{
    public class ProductInventoryUpdatedEvent : DomainEvent
    {
        public Guid ProductInventoryId { get; }
        public int ProductId { get; }
        public int QuantityOnHand { get; }

        public ProductInventoryUpdatedEvent(Guid productInventoryId, int productId, int quantityOnHand)
        {
            ProductInventoryId = productInventoryId;
            ProductId = productId;
            QuantityOnHand = quantityOnHand;
        }
    }
}

