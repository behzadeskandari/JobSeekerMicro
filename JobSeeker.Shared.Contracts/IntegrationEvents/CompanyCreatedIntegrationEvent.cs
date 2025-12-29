using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class CompanyCreatedIntegrationEvent(
        int CompanyId,
        string Name,
        string UserId,
        string? LogoUrl
    ) : IntegrationEvent;
}

