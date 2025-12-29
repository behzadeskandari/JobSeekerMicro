using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class JobSavedIntegrationEvent(
        int SavedJobId,
        string UserId,
        int JobPostId
    ) : IntegrationEvent;
}

