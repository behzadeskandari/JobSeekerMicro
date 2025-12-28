namespace JobSeeker.Shared.Contracts.Integration
{
    public class AdvertisementCreatedIntegrationEvent(
        Guid AdvertisementId,
        string UserId,
        int CompanyId,
        int CategoryId,
        string Title,
        bool IsPaid
    ) : IntegrationEvent;
}

