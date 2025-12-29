using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class InterviewScheduledIntegrationEvent(
        int InterviewId,
        int JobApplicationId,
        DateTime InterviewDate
    ) : IntegrationEvent;
}

