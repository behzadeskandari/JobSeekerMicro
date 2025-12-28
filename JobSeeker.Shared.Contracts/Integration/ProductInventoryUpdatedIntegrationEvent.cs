namespace JobSeeker.Shared.Contracts.Integration
{
    public class ProductInventoryUpdatedIntegrationEvent(
        Guid ProductInventoryId,
        int ProductId,
        int QuantityOnHand
    ) : IntegrationEvent;
}

