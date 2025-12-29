using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class OfferSentIntegrationEvent(
        int OfferId,
        int JobApplicationId,
        string UserId,
        int CompanyId,
        decimal? Salary
    ) : IntegrationEvent;
}

