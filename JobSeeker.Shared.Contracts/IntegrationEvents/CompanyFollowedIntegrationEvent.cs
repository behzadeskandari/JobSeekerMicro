using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class CompanyFollowedIntegrationEvent(
        int FollowId,
        string UserId,
        int CompanyId,
        DateTime FollowedAt
    ) : IntegrationEvent;
}

