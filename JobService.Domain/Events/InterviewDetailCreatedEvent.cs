using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class InterviewDetailCreatedEvent : DomainEvent
    {
        public int InterviewId { get; }
        public int ApplicationId { get; }
        public int InterviewerId { get; }
        public DateTime InterviewDate { get; }
        public string Type { get; }
        public DateTime CreatedAt { get; }

        public InterviewDetailCreatedEvent(int interviewId, int applicationId, int interviewerId, DateTime interviewDate, string type, DateTime createdAt)
        {
            InterviewId = interviewId;
            ApplicationId = applicationId;
            InterviewerId = interviewerId;
            InterviewDate = interviewDate;
            Type = type;
            CreatedAt = createdAt;
        }
    }
}

