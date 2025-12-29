using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class JobSavedIntegrationEvent(
        Guid SavedJobId,
        string UserId,
        int JobPostId
    ) : IntegrationEvent;
}

