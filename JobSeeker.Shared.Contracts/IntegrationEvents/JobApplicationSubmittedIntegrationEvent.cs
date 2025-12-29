using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class JobApplicationSubmittedIntegrationEvent(
        int JobApplicationId,
        int JobPostId,
        string UserId,
        string ResumeUrl,
        DateTime AppliedAt
    ) : IntegrationEvent;
}

