using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class JobApplicationRejectedIntegrationEvent(
        int JobApplicationId,
        string UserId,
        string? Reason
    ) : IntegrationEvent;
}

