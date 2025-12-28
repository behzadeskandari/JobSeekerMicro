namespace JobSeeker.Shared.Contracts.Integration
{
    public class OrderPlacedIntegrationEvent(
        Guid OrderId,
        int PricingPlanId,
        string? UserId,
        decimal TotalAmount
    ) : IntegrationEvent;
}

