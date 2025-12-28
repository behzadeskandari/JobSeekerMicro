namespace JobSeeker.Shared.Contracts.Integration
{
    public class PaymentProcessedIntegrationEvent(
        Guid PaymentId,
        int AdvertisementId,
        string UserId,
        decimal Amount,
        string Status
    ) : IntegrationEvent;
}

