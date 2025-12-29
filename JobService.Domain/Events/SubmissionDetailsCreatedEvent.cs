using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class SubmissionDetailsCreatedEvent : DomainEvent
    {
        public int SubmissionId { get; }
        public int ApplicationId { get; }
        public string Source { get; }
        public DateTime CreatedAt { get; }

        public SubmissionDetailsCreatedEvent(int submissionId, int applicationId, string source, DateTime createdAt)
        {
            SubmissionId = submissionId;
            ApplicationId = applicationId;
            Source = source;
            CreatedAt = createdAt;
        }
    }
}

