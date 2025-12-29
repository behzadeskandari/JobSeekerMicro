using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class InterviewDetailUpdatedEvent : DomainEvent
    {
        public int InterviewId { get; }
        public int ApplicationId { get; }
        public string Outcome { get; }
        public DateTime UpdatedAt { get; }

        public InterviewDetailUpdatedEvent(int interviewId, int applicationId, string outcome, DateTime updatedAt)
        {
            InterviewId = interviewId;
            ApplicationId = applicationId;
            Outcome = outcome;
            UpdatedAt = updatedAt;
        }
    }
}

