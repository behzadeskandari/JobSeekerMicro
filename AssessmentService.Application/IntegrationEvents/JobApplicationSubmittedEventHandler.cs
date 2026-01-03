using JobSeeker.Shared.Contracts.IntegrationEvents;
using JobSeeker.Shared.EventBusRabbitMQ;
using Microsoft.Extensions.Logging;

namespace AssessmentService.Application.IntegrationEvents
{
    public class JobApplicationSubmittedEventHandler(
        ILogger<JobApplicationSubmittedEventHandler> logger
    ) : IIntegrationEventHandler<JobApplicationSubmittedIntegrationEvent>
    {
        private readonly ILogger<JobApplicationSubmittedEventHandler> _logger = logger;

        public async Task HandleAsync(JobApplicationSubmittedIntegrationEvent @event)
        {
            _logger.LogInformation(
                "Received JobApplicationSubmittedIntegrationEvent: JobApplicationId={JobApplicationId}, JobPostId={JobPostId}, UserId={UserId}, AppliedAt={AppliedAt}",
                @event.JobApplicationId,
                @event.JobPostId,
                @event.UserId,
                @event.AppliedAt
            );

            // Here you could add business logic to handle the event
            // For example, trigger assessment notifications, update candidate profiles, etc.

            await Task.CompletedTask;
        }
    }
}
